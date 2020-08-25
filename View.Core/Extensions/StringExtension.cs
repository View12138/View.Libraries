﻿using System;
using System.Collections.Generic;
using System.Text;
using View.Core.Values;

namespace View.Core.Extensions
{
    /// <summary>
    /// 字符串扩展
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// 指示指定的字符串是 <see langword="null"/> 还是 <see cref="string.Empty"/> 字符串。
        /// </summary>
        /// <param name="value">指定的字符串</param>
        /// <returns>如果参数 <paramref name="value"/> 为 <see langword="null"/> 或 空字符串("")，则为 <see langword="true"/>；否则为 <see langword="false"/>。</returns>
        public static bool IsNullOrEmpty(this string value) => string.IsNullOrEmpty(value);
        /// <summary>
        /// 指示指定的字符串是 <see langword="null"/> 、空还是仅由空白字符串组成。
        /// </summary>
        /// <param name="value">指定的字符串</param>
        /// <returns>如果参数 <paramref name="value"/> 为 <see langword="null"/> ，空字符串("")或仅由空白字符组成，则为 <see langword="true"/>；否则为 <see langword="false"/>。</returns>
        public static bool IsNullOrWhiteSpace(this string value) => string.IsNullOrWhiteSpace(value);
        /// <summary> 
        /// 根据时间戳字符串获取 Utc时间。
        /// </summary>  
        public static DateTime ToDateTime(this string timeStamp)
        {
            if (long.TryParse(timeStamp, out long _timeStamp) || _timeStamp >= 0)
            { return DateTimeValue.UnixZero.AddMilliseconds(_timeStamp); }
            else
            { throw new ArgumentOutOfRangeException(nameof(timeStamp), "字符串无法转换为 Unix时间戳，或 Unix时间戳 小于 0。"); }
        }
    }
}
