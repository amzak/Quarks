using System;
using System.Collections.Generic;

namespace Codestellation.Quarks.Collections
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> self) => self ?? Array.Empty<T>();

        public static string ToJoinedString<T>(this IEnumerable<T> self, string separator = ",") => string.Join(separator, self);
    }
}