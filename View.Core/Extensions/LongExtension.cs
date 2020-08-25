using System;
using View.Core.Values;

namespace View.Core.Extensions
{
    /// <summary>
    /// <see cref="long"/> 扩展类。
    /// </summary>
    public static class LongExtension
    {
        /// <summary> 
        /// 根据时间戳获取 Utc时间。
        /// </summary>  
        public static DateTime ToDateTime(this long timeStamp)
        {
            if (timeStamp >= 0)
            { return DateTimeValue.UnixZero.AddMilliseconds(timeStamp); }
            else
            { throw new ArgumentOutOfRangeException(nameof(timeStamp), "Unix时间戳 不能小于 0。"); }
        }
    }
}
