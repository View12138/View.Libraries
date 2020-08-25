using System;
using View.Core.Values;

namespace View.Core.Extensions
{
    /// <summary>
    /// <see cref="DateTime"/> 扩展类。
    /// </summary>
    public static class DateTimeExtension
    {
        /// <summary>
        /// 将时间转换为 Unix 时间戳
        /// </summary>
        /// <param name="dateTime">给定的时间</param>
        /// <returns></returns>
        public static long ToUnixTimeStamp(this DateTime dateTime)
        {
            return Convert.ToInt64(dateTime.ToUniversalTime().Subtract(DateTimeValue.UnixZero).TotalMilliseconds);
        }

        /// <summary>
        /// 日期是否为空 或 默认时间(0001-1-1 00:00:00)
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static bool IsNull(this DateTime dateTime)
        {
            return dateTime == null || dateTime == DateTimeValue.Null || dateTime.ToUniversalTime() == DateTimeValue.Null;
        }

        /// <summary>
        /// 日期是否为空 或 默认时间(0001-1-1 00:00:00) 或 过期 (即指定的日期是否已经超过了此时此刻)
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static bool IsNullOrOverdue(this DateTime dateTime)
        {
            return dateTime.IsNull() || dateTime.ToUniversalTime() < DateTime.UtcNow;
        }
    }
}
