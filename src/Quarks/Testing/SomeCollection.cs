using System;
using System.Collections.Generic;

namespace Codestellation.Quarks.Testing
{
    public static partial class Some
    {
        public static T ElementOf<T>(IList<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }

            if (list.Count == 0)
            {
                throw new ArgumentException("The list is empty", "list");
            }

            var someIndex = InternalIndexOf(list);
            return list[someIndex];
        }

        private static int InternalIndexOf<T>(IList<T> list)
        {
            var index = Int32(0, list.Count - 1);
            return index;
        }

        public static int IndexOf<T>(IList<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }

            if (list.Count == 0)
            {
                throw new ArgumentException("The list is empty", "list");
            }

            return InternalIndexOf(list);
        }
    }
}