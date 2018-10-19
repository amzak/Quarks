using System;
using System.Threading;

namespace Codestellation.Quarks.Threading
{
    public static class ThreadExtensions
    {
        public static readonly TimeSpan InfiniteTimeout = TimeSpan.FromMilliseconds(-1);

        public static bool SafeJoin(this Thread self, TimeSpan? timeOut = null)
        {
            if (self == null)
            {
                return true;
            }

            if (self.ThreadState == ThreadState.Unstarted)
            {
                return true;
            }

            if (self == Thread.CurrentThread)
            {
                //deadlock possible otherwise
                return true;
            }

            return self.Join(timeOut ?? InfiniteTimeout);
        }
    }
}