﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Timers;
using View.AutoTask.Core;
using View.AutoTask.Options;
using View.AutoTask.Caches;

namespace View.AutoTask
{
    public static partial class TaskInitializer
    {
        private struct Tasks
        {
            public Tasks(MethodInfo task, TaskMethodAttribute info)
            {
                Task = task;
                Info = info;
            }

            public MethodInfo Task { get; private set; }
            public TaskMethodAttribute Info { get; private set; }
        };

        private static readonly List<Tasks> tasks = new List<Tasks>();
        private static readonly Timer timer = new Timer(400);
        private static HashSet<Metadata> metadatas = new HashSet<Metadata>();

        public static TaskOptions Options
        {
            get => Default.Options; set
            {
                Default.Options = value ?? new TaskOptions();
                Default.Options.Cache = Default.Options.Cache ?? new DefaultMemonyMetadataCache();
            }
        }

        /// <summary>
        /// 扫描并启动任务
        /// </summary>
        /// <param name="options"></param>
        public static void Start(TaskOptions options = null)
        {
            Options = options;
            AppDomain.CurrentDomain.ProcessExit += (s, e) => Default.Options.Cache.WriteMetadatas(metadatas);
            InitializationTask();
        }

        internal static void InitializationTask()
        {
            var now = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            Debug.WriteLine($"{now}: 任务初始化");
            metadatas = Default.Options.Cache.ReadMetadatas();

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            if (Default.Options.TaskScope == TaskScope.Assembly)
            {
                assemblies = assemblies.Where(x => x.CustomAttributes.Select(x => x.AttributeType).Contains(typeof(TaskAssemblyAttribute))).ToArray();
            }

            foreach (Assembly assembly in assemblies)
            {
                Type[] taskGroups = assembly.GetTypes();
                if (Default.Options.TaskScope == TaskScope.Assembly || Default.Options.TaskScope == TaskScope.Class)
                {
                    taskGroups = taskGroups.Where(x => x.CustomAttributes.Select(x => x.AttributeType).Contains(typeof(TaskClassAttribute))).ToArray();
                }
                foreach (var taskGroup in taskGroups)
                {
                    var _tasks = taskGroup.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                        .Where(x => x.CustomAttributes.Select(x => x.AttributeType).Contains(typeof(TaskMethodAttribute)));
                    foreach (var task in _tasks)
                    {
                        var info = task.GetCustomAttribute<TaskMethodAttribute>();
                        var moduleToken = task.Module.MetadataToken;
                        var methodToken = task.MetadataToken;

                        if (metadatas.TryGetValue(new Metadata(moduleToken, methodToken), out Metadata metadata))
                        {
                            info.Excuted(metadata.ExcuteTime);
                        }
                        else
                        {
                            metadatas.Add(new Metadata(moduleToken, methodToken, now));
                            info.Excuted(now);
                        }
                        tasks.Add(new Tasks(task, info));
                        Debug.WriteLine($"{now}: 找到一个自动任务 - {info.TaskName}");
                    }
                }
            }

            tasks.Where(x => x.Info.OnStartup).ToList().ForEach(task => task.Excute(now));
            timer.Elapsed += (s, e) =>
            {
                var now = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                foreach (var task in tasks)
                {
                    if (task.Info.CanExcute())
                    {
                        task.Excute(now);
                    }
                }
            };
            timer.Start();
        }


        #region Extensions

        static async void Excute(this Tasks task, long now)
        {
            for (int i = 0; i <= task.Info.Retry; i++)
            {
                try
                {
                    task.Task.Invoke(null, null);
                    task.Info.Excuted(now);
                    var _metadata = new Metadata(task.Task, now);
                    if (metadatas.TryGetValue(_metadata, out Metadata metadata))
                    { metadata.ExcuteTime = now; }
                    else
                    { metadatas.Add(_metadata); }
                    Debug.WriteLine($"{DateTime.Now}: {task.Info.TaskName} 已执行。");
                    return;
                }
                catch (TaskExcuteException)
                {
                    if (i >= task.Info.Retry) { return; }
                    await Task.Delay(1000 * (int)Math.Pow(2, i + 1));
                }
                catch (Exception ex)
                when (ex is TargetException ||
                      ex is ArgumentException ||
                      ex is TargetInvocationException ||
                      ex is TargetParameterCountException ||
                      ex is MethodAccessException ||
                      ex is InvalidOperationException ||
                      ex is NotSupportedException)
                { return; }
            }
            return;
        }
        #endregion
    }
}
