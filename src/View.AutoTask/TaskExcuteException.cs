using System;

namespace View.AutoTask
{
    /// <summary>
    /// 任务执行异常
    /// <para>可在 <see cref="TaskMethodAttribute"/> 修饰的方法中抛出此异常以重试。重试次数由 <see cref="TaskMethodAttribute.Retry"/> 决定</para>
    /// </summary>
    [Serializable]
    public class TaskExcuteException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public TaskExcuteException() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public TaskExcuteException(string message) : base(message) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public TaskExcuteException(string message, Exception inner) : base(message, inner) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected TaskExcuteException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

}
