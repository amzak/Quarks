using System.Collections.Generic;
using System.Linq;

namespace Codestellation.Common.Extentions
{
    public static class EnumerableExt
    {
        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> self)
        {
            return self ?? Enumerable.Empty<T>();
        }
    }
}