using System;
using View.Core.Models;
using View.Core.Values;

namespace View.Core.Extensions
{
    /// <summary>
    /// <see cref="DateTime"/> 扩展类。
    /// </summary>
    public static class DateTimeExtension
    {
        /// <summary>
        /// 日期是否为默认时间(0001-1-1 00:00:00)
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static bool IsDefault(this DateTime dateTime) => dateTime == default;
    }
}
