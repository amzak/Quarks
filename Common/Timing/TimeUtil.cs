using System;

namespace Codestellation.Common.Timing
{
    public static class TimeUtil
    {
        public static DateTimeOffset TruncateMilliseconds(DateTimeOffset dateTimeOffset)
        {
            return dateTimeOffset.AddMilliseconds(-dateTimeOffset.Millisecond);
        }
    }
}