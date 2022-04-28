using System;

namespace View.AutoTask.Core
{
    internal static class TaskExtensions
    {
        public static readonly TimeSpan Start = TimeSpan.Zero;
        public static readonly TimeSpan Stop = new(0, 23, 59, 59, 999);

        public static void ToTime(this string time, out TimeSpan start, out TimeSpan stop)
        {
            try
            {
                var times = time.Split('-');
                start = TimeSpan.Parse(times[0]);
                stop = TimeSpan.Parse(times[1]);
                if (start.TotalMilliseconds > 86399999 || stop.TotalMilliseconds > 86399999)
                { throw new ArgumentOutOfRangeException(nameof(time), "起止时间不能超过24小时(86399999 毫秒)"); }
                if (start >= stop)
                { throw new ArgumentOutOfRangeException(nameof(time), "结束时间不能小于开始时间"); }
            }
            catch (Exception ex)
            when (ex is IndexOutOfRangeException ||
                  ex is ArgumentOutOfRangeException ||
                  ex is ArgumentNullException ||
                  ex is FormatException ||
                  ex is OverflowException)
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
