using System;

namespace Codestellation.Common.Timing
{
    public class TestClock : IClock
    {
        public TestClock()
            : this(TruncateMilliseconds(DateTimeOffset.Now))
        {
            
        }

        private static DateTimeOffset TruncateMilliseconds(DateTimeOffset now)
        {
            return now.AddMilliseconds(-now.Millisecond);
        }

        public TestClock(DateTimeOffset now)
        {
            Now = now;
        }

        public DateTimeOffset Now { get; set; }
    }
}