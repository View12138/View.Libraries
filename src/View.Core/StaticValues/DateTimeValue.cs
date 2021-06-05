using System;
using System.Collections.Generic;
using System.Text;

namespace View.Core.StaticValues
{
    /// <summary>
    /// 日期的默认值
    /// </summary>
    public static class DateTimeValue
    {
        /// <summary>
        /// Unix 时间戳 0 的值。
        /// <para>ISO 8601规范 : 1970-01-01T00:00:00Z</para>
        /// </summary>
        public static DateTime UnixZero => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// 标准时间格式化字符串。符合 ISO 8601 标准
        /// <para><see langword="yyyy-MM-ddTHH:mm:ss.fffffffK"/></para>
        /// </summary>
        public static string StandardFormat => "O";
        
        /// <summary>
        /// 通用的时间格式化字符串。
        /// <para><see langword="yyyy-MM-dd HH:mm:ss"/></para>
        /// </summary>
        public static string NormalFormat => "yyyy-MM-dd HH:mm:ss";
    }
}
