using System;

namespace Codestellation.Common.Timing
{
    /// <summary>
    /// Implementation of <see cref="IClock"/> for testing. 
    /// <remarks>It does not change time until its changed by user. It's all you need mostly.</remarks> 
    /// </summary>
    public class TestClock : IClock
    {
        /// <summary>
        /// Initializes clock with current date and time without milliseconds.
        /// </summary>
        public TestClock()
            : this(TimeUtil.TruncateMilliseconds(DateTimeOffset.Now))
        {
            
        }
        
        /// <summary>
        /// Initializes clock with supplied date and time.
        /// </summary>
        /// <param name="now">Current date and time.</param>
        public TestClock(DateTimeOffset now)
        {
            Now = now;
        }

        /// <summary>
        /// Gets of sets current date and time.
        /// </summary>
        public DateTimeOffset Now { get; set; }
    }
}