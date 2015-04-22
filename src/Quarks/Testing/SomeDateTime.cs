using System;

namespace Codestellation.Quarks.Testing
{
    internal static partial class Some
    {
        public static DateTime DateTime()
        {
            var min = System.DateTime.MinValue;
            var max = System.DateTime.MaxValue;

            return DateTime(min, max);
        }

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

        public static DateTime DateTimeGreaterThan(DateTime specified)
        {
            if (specified == System.DateTime.MaxValue)
            {
                throw new ArgumentOutOfRangeException(
                    "specified",
                    "Cannot create DateTime greater than DateTime.MaxValue");
            }

            var min = specified.AddTicks(1);
            var max = System.DateTime.MaxValue;

            return DateTime(min, max);
        }

        public static DateTime DateTimeLessThan(DateTime specified)
        {
            if (specified == System.DateTime.MinValue)
            {
                throw new ArgumentOutOfRangeException(
                    "specified",
                    "Cannot create DateTime less than DateTime.MinValue");
            }

            var min = System.DateTime.MinValue;
            var max = specified.AddTicks(-1);

            return DateTime(min, max);
        }

        public static DateTime DateTime(long ticks)
        {
            return System.DateTime.FromFileTime(ticks);
        }
    }
}