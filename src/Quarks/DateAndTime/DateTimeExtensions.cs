using System;

namespace Codestellation.Quarks.DateAndTime
{
    public static class DateTimeExtensions
    {
        public static DateTime DiscardMilliseconds(this DateTime time)
            => new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, time.Second, time.Kind);

        public static DateTime DiscardMicroseconds(this DateTime time)
            => new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, time.Second, time.Millisecond, time.Kind);
    }
}