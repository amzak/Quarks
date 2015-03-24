using System;

namespace Codestellation.Quarks.Testing
{
    internal static partial class Some
    {
        public static uint UInt32(uint min, uint max)
        {
            if (min > max)
            {
                throw new ArgumentException("Min value must be less or equal to max value");
            }

            var randomDouble = Double(min, max);
            return (uint) Math.Round(randomDouble);
        }

        public static uint UInt32()
        {
            return UInt32(uint.MinValue, uint.MaxValue);
        }

        public static uint PositiveUInt32(uint max = uint.MaxValue)
        {
            return UInt32(1, max);
        }
    }
}