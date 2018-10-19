using Microsoft.Win32.SafeHandles;

namespace Codestellation.Quarks.Native
{
    public class LibraryHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        public string LibraryPath;

        public LibraryHandle() : base(true)
        {

        }

        protected override bool ReleaseHandle()
        {
            Platform.FreeLibraryEx(this);
            LibraryPath = null;
            return true;
        }
    }
}