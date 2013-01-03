using System;

namespace Codestellation.Common.Timing
{
    /// <summary>
    /// Represents a concept of current datetime picker.
    /// </summary>
    public class RealClock : IClock
    {
        /// <summary>
        /// Gets an intance of <see cref="RealClock"/>
        /// </summary>
        public static readonly RealClock Instance = new RealClock();


        /// <summary>
        /// Gets current <see cref="DateTimeOffset"/>.
        /// <remarks>Simply returns value of DateTimeOffset.Now.</remarks>
        /// </summary>
        public DateTimeOffset Now
        {
            get { return DateTimeOffset.Now; }
        }
    }
}