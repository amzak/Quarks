using System;

namespace Codestellation.Quarks.Testing
{
    public static partial class Some
    {
        public static double Double(double min, double max)
        {
            if (min > max)
            {
                throw new ArgumentException("Min value must be less or equal to max value");
            }

            var delta = max - min;
            if (double.IsInfinity(delta))
            {
                delta = double.MaxValue;
            }

            var summand = Random.NextDouble() * delta;
            var result = min + summand;

            return result;
        }

        public static double Double()
        {
            return Double(double.MinValue, double.MaxValue);
        }

        public static double PositiveDouble(double max = double.MaxValue)
        {
            return Double(0.0, max);
        }
    }
}