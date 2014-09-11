using System.Collections.Generic;

namespace Codestellation.Quarks.Collections
{
    internal static class EnumerableExtensions
    {
        private static class ArrayOf<T>
        {
             public static readonly T[] Empty = new T[0];
        }
        
        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> self)
        {
            return self ?? ArrayOf<T>.Empty;
        }
    }
}