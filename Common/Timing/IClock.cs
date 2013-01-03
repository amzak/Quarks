using System;

namespace Codestellation.Common.Timing
{
    /// <summary>
    /// Represents concept of a clock.
    /// </summary>
    public interface IClock
    {
        /// <summary>
        /// Gets current  <see cref="DateTimeOffset"/>.
        /// </summary>
        DateTimeOffset Now { get; }
    }
}