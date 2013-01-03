using System;

namespace Codestellation.Common.Timing
{
    public interface IClock
    {
        DateTimeOffset Now { get; }
    }
}