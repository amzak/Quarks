using System;

namespace Codestellation.Common.Timing
{
    public class RealClock : IClock
    {
        public static readonly RealClock Instance = new RealClock();

        public DateTimeOffset Now
        {
            get { return DateTimeOffset.Now; }
        }
    }
}