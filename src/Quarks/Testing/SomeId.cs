using System.Threading;

namespace Codestellation.Quarks.Testing
{
    public static partial class Some
    {
        private static int _nextId;

        public static int Id() => Interlocked.Increment(ref _nextId);
    }
}