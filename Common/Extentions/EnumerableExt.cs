using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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