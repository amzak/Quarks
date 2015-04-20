using System;

namespace Codestellation.Quarks.Testing
{
    internal static partial class Some
    {
        public static T Enum<T>()
            where T : struct, IComparable, IFormattable, IConvertible
        {
            var type = typeof(T);
            var result = (T)Enum(type);

            return result;
        }

        public static object Enum(Type enumType)
        {
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("T should be an enumerated type");
            }

            Array values = System.Enum.GetValues(enumType);
            int index = Int32(0, values.Length - 1);
            return values.GetValue(index);
        }
    }
}