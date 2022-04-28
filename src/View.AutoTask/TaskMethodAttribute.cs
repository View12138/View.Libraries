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
    public sealed class TaskMethodAttribute : Attribute, IEquatable<TaskMethodAttribute>
    {
        private readonly TimeSpan start = TaskExtensions.Start;
        private readonly TimeSpan stop = TaskExtensions.Stop;
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
        public bool OnStartup { get; init; } = false;

        /// <summary>
        /// 任务运行的时间段
        /// <para>默认值 "<c>00:00:00.000-23:59:59.999</c>" </para>
        /// </summary>
        public string RunSpan { get => $"{start:HH:mm:dd.zzz}-{stop:HH:mm:dd.zzz}"; init { (start, stop) = value.ToTime(); } }

        /// <summary>
        /// 任务失败时重试次数
        /// </summary>
        public uint Retry { get; set; } = 3;

        #region HashCode & IEquatable

        /// <summary>
        /// Returns a value that indicates whether this instance is equal to a specified object.
        /// </summary>
        /// <param name="obj">An <see cref="System.Object"/> to compare with this instance or null.</param>
        /// <returns>true if obj and this instance are of the same type and have identical field values; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as TaskMethodAttribute);
        }

        /// <summary>
        /// Returns a value that indicates whether this instance is equal to a specified object.
        /// </summary>
        /// <param name="other">An <see cref="TaskMethodAttribute"/> to compare with this instance or null.</param>
        /// <returns>true if obj and this instance are of the same type and have identical field values; otherwise, false.</returns>
        public bool Equals(TaskMethodAttribute other)
        {
            return other != null &&
                               base.Equals(other) &&
                               EqualityComparer<object>.Default.Equals(TypeId, other.TypeId) &&
                               start.Equals(other.start) &&
                               stop.Equals(other.stop) &&
                               interval == other.interval &&
                               lastUnixTime == other.lastUnixTime &&
                               TaskName == other.TaskName &&
                               OnStartup == other.OnStartup &&
                               RunSpan == other.RunSpan;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(base.GetHashCode());
            hash.Add(TypeId);
            hash.Add(start);
            hash.Add(stop);
            hash.Add(interval);
            hash.Add(lastUnixTime);
            hash.Add(TaskName);
            hash.Add(OnStartup);
            hash.Add(RunSpan);
            return hash.ToHashCode();
        }

        #endregion

    }
}
