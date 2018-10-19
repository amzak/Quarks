using System;

namespace Codestellation.Quarks.Testing
{
    public static partial class Some
    {
        public static long Int64(long min, long max)
        {
            if (min > max)
            {
                throw new ArgumentException("Min value must be less or equal to max value");
            }

            if (min == max)
            {
                return min;
            }

            var randomDecimal = Decimal(min, max);
            return (long)Math.Round(randomDecimal);
        }

        public static long Int64()
        {
            return Int64(long.MinValue, long.MaxValue);
        }

        public static long PositiveInt64(long max = long.MaxValue)
        {
            return Int64(1, max);
        }
    }
}