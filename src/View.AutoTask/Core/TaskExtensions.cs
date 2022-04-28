using System;

namespace View.AutoTask.Core
{
    internal static class TaskExtensions
    {
        public static readonly TimeSpan Start = TimeSpan.Zero;
        public static readonly TimeSpan Stop = new(0, 23, 59, 59, 999);

        public static (TimeSpan Start, TimeSpan Stop) ToTime(this string time)
        {
            try
            {
                var times = time.Split("-");
                var start = TimeSpan.Parse(times[0]);
                var end = TimeSpan.Parse(times[1]);
                if (start.TotalMilliseconds > 86399999 || end.TotalMilliseconds > 86399999)
                { throw new ArgumentOutOfRangeException(nameof(time), "起止时间不能超过24小时(86399999 毫秒)"); }
                if (start >= end)
                { throw new ArgumentOutOfRangeException(nameof(time), "结束时间不能小于开始时间"); }

                return (start, end);
            }
            catch (Exception ex) when (ex is IndexOutOfRangeException or ArgumentOutOfRangeException or ArgumentNullException or FormatException or OverflowException)
            {
                throw new ApplicationException($"时间格式错误：{time}。", ex);
            }
        }

        public static bool InTimeSpan(this DateTime time, TimeSpan start, TimeSpan end)
        {
            var now = time.TimeOfDay;
            if (start <= end)
            {
                return start < now && now <= end;
            }
            else return false;
        }
    }
}
