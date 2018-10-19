using System;

namespace Codestellation.Quarks.Testing
{
    public static partial class Some
    {
        public static decimal Decimal(decimal min, decimal max)
        {
            if (min > max)
            {
                throw new ArgumentException("Min value must be less or equal to max value");
            }

            decimal delta;
            try
            {
                delta = checked (max - min);
            }
            catch (OverflowException)
            {
                delta = decimal.MaxValue;
            }

            if (delta == 0m)
            {
                return min;
            }

            var factor = (decimal) Random.NextDouble();
            var summand = factor * delta;
            var result = Int32(0, 1) == 1
                ? min + summand
                : max - summand;

            return result;
        }

        public static decimal Decimal()
        {
            return Decimal(decimal.MinValue, decimal.MaxValue);
        }

        public static decimal PositiveDecimal(decimal max = decimal.MaxValue)
        {
            return Decimal(0.0m, max);
        }
    }
}