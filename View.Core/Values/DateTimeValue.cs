using System;
using System.Collections.Generic;
using System.Text;

namespace View.Core.Values
{
    /// <summary>
    /// 日期的默认值
    /// </summary>
    public static class DateTimeValue
    {
        /// <summary>
        /// new <see cref="DateTime"/> 的值。
        /// </summary>
        public static DateTime Null => new DateTime();

        /// <summary>
        /// Unix 时间戳 0 的值。
        /// <para>ISO 8601规范 : 1970-01-01T00:00:00Z</para>
        /// </summary>
        public static DateTime UnixZero => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    }
}
