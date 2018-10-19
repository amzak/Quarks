using System;

namespace Codestellation.Quarks.Testing
{
    public static partial class Some
    {
        public static int Int32(int min, int max)
        {
            if (min > max)
            {
                throw new ArgumentException("Min value must be less or equal to max value");
            }

            // this check is not necessary, yet it improves the quality of the output distribution
            if (min == max)
            {
                return min;
            }

            return max == int.MaxValue
                ? Random.Next(min, max)
                : Random.Next(min, max + 1);
        }

        public static int Int32()
        {
            return Int32(int.MinValue, int.MaxValue);
        }

        public static int PositiveInt32(int max = int.MaxValue)
        {
            return Int32(1, max);
        }
    }
}