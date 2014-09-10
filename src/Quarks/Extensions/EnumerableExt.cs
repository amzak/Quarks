using System.Collections.Generic;
using System.Linq;

namespace Codestellation.Quarks.Extentions
{
    internal static class EnumerableExt
    {
        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> self)
        {
            return self ?? Enumerable.Empty<T>();
        }
    }
}