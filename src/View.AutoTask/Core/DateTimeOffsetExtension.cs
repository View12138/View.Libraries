using System;

namespace View.AutoTask.Core
{

#if !NETCOREAPP1_0_OR_GREATER && !NET46_OR_GREATER && !NETSTANDARD1_3_OR_GREATER
    // Author : View (mailto:view96@qq.com)
    // Time : 2022年4月28日 15点20分
    /// <summary>
    /// <see cref="DateTimeOffset"/> 扩展类。
    /// <para>from .net core github repository</para>
    /// </summary>
    internal static class DateTimeOffsetExtension
    {
        #region DateTime Constants
        // Number of 100ns ticks per time unit
        private const long TicksPerMillisecond = 10000;
        private const long TicksPerSecond = TicksPerMillisecond * 1000;
        private const long TicksPerMinute = TicksPerSecond * 60;
        private const long TicksPerHour = TicksPerMinute * 60;
        private const long TicksPerDay = TicksPerHour * 24;

        internal const long MinTicks = 0;
        internal const long MaxTicks = DaysTo10000 * TicksPerDay - 1;

        private const int DaysPerYear = 365;
        // Number of days in 4 years
        private const int DaysPer4Years = DaysPerYear * 4 + 1;       // 1461
        // Number of days in 100 years
        private const int DaysPer100Years = DaysPer4Years * 25 - 1;  // 36524
        // Number of days in 400 years
        private const int DaysPer400Years = DaysPer100Years * 4 + 1; // 146097

        // Number of days from 1/1/0001 to 12/31/1600
        private const int DaysTo1601 = DaysPer400Years * 4;          // 584388
        // Number of days from 1/1/0001 to 12/30/1899
        private const int DaysTo1899 = DaysPer400Years * 4 + DaysPer100Years * 3 - 367;
        // Number of days from 1/1/0001 to 12/31/1969
        internal const int DaysTo1970 = DaysPer400Years * 4 + DaysPer100Years * 3 + DaysPer4Years * 17 + DaysPerYear; // 719,162
        // Number of days from 1/1/0001 to 12/31/9999
        private const int DaysTo10000 = DaysPer400Years * 25 - 366;  // 3652059
        internal const long UnixEpochTicks = DaysTo1970 * TicksPerDay;
        #endregion

        #region DateTimeOffset Constants
        internal const long MaxOffset = TimeSpan.TicksPerHour * 14;
        internal const long MinOffset = -MaxOffset;

        private const long UnixEpochSeconds = UnixEpochTicks / TimeSpan.TicksPerSecond; // 62,135,596,800
        private const long UnixEpochMilliseconds = UnixEpochTicks / TimeSpan.TicksPerMillisecond; // 62,135,596,800,000

        internal const long UnixMinSeconds = MinTicks / TimeSpan.TicksPerSecond - UnixEpochSeconds;
        internal const long UnixMaxSeconds = MaxTicks / TimeSpan.TicksPerSecond - UnixEpochSeconds;
        #endregion

        /// <summary>
        /// 返回自 1970-01-01T00:00:00.000Z 至 <paramref name="offset"/> 所经过的秒数。
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static long ToUnixTimeSeconds(this DateTimeOffset offset)
        {
            // Truncate sub-second precision before offsetting by the Unix Epoch to avoid
            // the last digit being off by one for dates that result in negative Unix times.
            //
            // For example, consider the DateTimeOffset 12/31/1969 12:59:59.001 +0
            //   ticks            = 621355967990010000
            //   ticksFromEpoch   = ticks - DateTime.UnixEpochTicks          = -9990000
            //   secondsFromEpoch = ticksFromEpoch / TimeSpan.TicksPerSecond = 0
            //
            // Notice that secondsFromEpoch is rounded *up* by the truncation induced by integer division,
            // whereas we actually always want to round *down* when converting to Unix time. This happens
            // automatically for positive Unix time values. Now the example becomes:
            //   seconds          = ticks / TimeSpan.TicksPerSecond = 62135596799
            //   secondsFromEpoch = seconds - UnixEpochSeconds      = -1
            //
            // In other words, we want to consistently round toward the time 1/1/0001 00:00:00,
            // rather than toward the Unix Epoch (1/1/1970 00:00:00).
            long seconds = offset.UtcDateTime.Ticks / TimeSpan.TicksPerSecond;
            return seconds - UnixEpochSeconds;
        }

        /// <summary>
        /// 返回自 1970-01-01T00:00:00.000Z 至 <paramref name="offset"/> 所经过的毫秒数。
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static long ToUnixTimeMilliseconds(this DateTimeOffset offset)
        {
            // Truncate sub-millisecond precision before offsetting by the Unix Epoch to avoid
            // the last digit being off by one for dates that result in negative Unix times
            long milliseconds = offset.UtcDateTime.Ticks / TimeSpan.TicksPerMillisecond;
            return milliseconds - UnixEpochMilliseconds;
        }
    }
#endif

}