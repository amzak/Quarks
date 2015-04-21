using System;

namespace Codestellation.Quarks.Testing
{
    internal static partial class Some
    {
        public static DateTime DateTime(DateTime min, DateTime max)
        {
            if (min > max)
            {
                throw new ArgumentException("Min value must be less or equal to max value");
            }

            if (min == max)
            {
                return min;
            }

            long binaryTime = Int64(min.Ticks, max.Ticks);
            return System.DateTime.FromBinary(binaryTime);
        }
    }
}