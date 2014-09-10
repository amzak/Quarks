using Microsoft.Win32.SafeHandles;

namespace Codestellation.Common.Native
{
    internal class MethodHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        public MethodHandle() : base(false)
        {
        }

        protected override bool ReleaseHandle()
        {
            return false;
        }
    }
}