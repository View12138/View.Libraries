using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System;
using View.AutoTask.Options;
using View.AutoTask.Core;

namespace View.AutoTask
{
    /// <summary>
    /// 自动任务。
    /// <para>任务以无参无返回值的公共静态方法为单元。</para>
    /// <para></para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class TaskMethodAttribute : Attribute
    {
        private TimeSpan start = TaskExtensions.Start;
        private TimeSpan stop = TaskExtensions.Stop;
        private readonly long interval;
        private long lastUnixTime;

        /// <summary>
        /// 创建一个自动任务。
        /// </summary>
        /// <param name="time">时间间隔时长。(由单位决定)</param>
        /// <param name="unit">时间间隔单位。</param>
        /// <param name="autoTaskName">任务名称</param>
        public TaskMethodAttribute(ushort time, Units unit, [CallerMemberName] string autoTaskName = "")
        {
            TaskName = autoTaskName;
            lastUnixTime = 0;
            this.interval = time * (unit switch
            {
                Units.Second => 1L * 1000,
                Units.Minute => 1L * 1000 * 60,
                Units.Hour => 1L * 1000 * 60 * 60,
                Units.Day => 1L * 1000 * 60 * 60 * 24,
                Units.Week => 1L * 1000 * 60 * 60 * 24 * 7,
                Units.Year => (long)(1L * 1000 * 60 * 60 * 24 * 365.25),
                _ => throw new NotSupportedException("不支持的枚举类型")
            });
        }

        /// <summary>
        /// 获取一个值, 该值标识任务在当前时间点是否可以执行
        /// </summary>
        /// <returns></returns>
        internal bool CanExcute()
        {
            var now = DateTime.Now;
            var nowUnixTime = new DateTimeOffset(now).ToUnixTimeMilliseconds();
            return now.InTimeSpan(start, stop) && nowUnixTime - interval > lastUnixTime;
        }

        internal void Excuted(DateTime? excuted = null)
        {
            lastUnixTime = new DateTimeOffset(excuted ?? DateTime.Now).ToUnixTimeMilliseconds();
        }
        internal void Excuted(long excuted) => lastUnixTime = excuted;

        /// <summary>
        /// 自动任务名称
        /// <para>默认是调用方法的名称</para>
        /// </summary>
        public string TaskName { get; }

        /// <summary>
        /// 当程序启动时是否立即运行一次
        /// <para>默认 : <see langword="false"/></para>
        /// </summary>
        public bool OnStartup { get; set; } = false;

        /// <summary>
        /// 任务运行的时间段
        /// <para>默认值 "<c>00:00:00.000-23:59:59.999</c>" </para>
        /// </summary>
        public string RunSpan { get => $"{start:HH:mm:dd.zzz}-{stop:HH:mm:dd.zzz}"; set => value.ToTime(out start, out stop); }

        /// <summary>
        /// 任务失败时重试次数
        /// </summary>
        public uint Retry { get; set; } = 3;

    }
}
