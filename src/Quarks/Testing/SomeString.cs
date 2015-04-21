using System;

namespace Codestellation.Quarks.Testing
{
    internal static partial class Some
    {
        public static readonly SomeStringOptions StringOptions = new SomeStringOptions();

        public static string String()
        {
            return String(StringOptions.DefaultLength);
        }

        public static string String(int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException("length", "String length should be non-negative");
            }

            var result = new char[length];
            var alphabet = StringOptions.Alphabet;

            for (int i = 0; i < length; i++)
            {
                result[i] = ElementOf(alphabet);
            }

            return new string(result);
        }

        public static string String(string prefix)
        {
            return String(prefix, StringOptions.DefaultLength);
        }

        public static string String(string prefix, int length)
        {
            var s = String(length);
            return !string.IsNullOrEmpty(prefix)
                ? string.Format("{0}{1}{2}", prefix, StringOptions.Delimiter, s)
                : s;
        }
    }
}