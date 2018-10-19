using System;

namespace Codestellation.Quarks.Testing
{
    public static partial class Some
    {
        private static readonly Random Random;

        static Some()
        {
            var guid = System.Guid.NewGuid();
            var seed = guid.GetHashCode();
            Random = new Random(seed);
        }
    }
}