using System;

namespace Codestellation.Quarks.DateAndTime
{
    public static class DateTimeExtensions
    {
        public static DateTime DiscardMilliseconds(this DateTime time)
        {
            return time.AddMilliseconds(-time.Millisecond);
        }      
    }
}