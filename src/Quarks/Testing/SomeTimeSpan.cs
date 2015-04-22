using System;

namespace Codestellation.Quarks.Testing
{
    internal static partial class Some
    {
        public static TimeSpan TimeSpan(TimeSpan min, TimeSpan max)
        {
            if (min > max)
            {
                throw new ArgumentException("Min value must be less or equal to max value");
            }

            if (min == max)
            {
                return min;
            }

            long randomTicks = Int64(min.Ticks, max.Ticks);
            return System.TimeSpan.FromTicks(randomTicks);
        }

        public static TimeSpan PositiveTimeSpan(TimeSpan max)
        {
            if (max <= System.TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException("max", "Max value must be greater than zero");
            }

            return TimeSpan(System.TimeSpan.Zero, max);
        }

        public static TimeSpan PositiveTimeSpan()
        {
            return PositiveTimeSpan(System.TimeSpan.MaxValue);
        }
    }
}