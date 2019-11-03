using Microsoft.Win32.SafeHandles;

namespace Codestellation.Quarks.Native
{
    public class MethodHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        public MethodHandle() : base(false)
        {
        }

        protected override bool ReleaseHandle() => false;
    }
}