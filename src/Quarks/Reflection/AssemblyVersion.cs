using System.Reflection;

namespace Codestellation.Quarks.Reflection
{
    public static class AssemblyVersion
    {
        public static readonly string InformationalVersion;

        public static readonly string Version;
        
        static AssemblyVersion()
        {
            Assembly entryAssembly = Assembly.GetEntryAssembly();

            var asmInfoVersion = entryAssembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
            var asmVersion = entryAssembly.GetCustomAttribute<AssemblyVersionAttribute>();

            InformationalVersion = asmInfoVersion == null ? "unknown" : asmInfoVersion.InformationalVersion;
            Version = asmVersion == null ? "unknown" : asmVersion.Version;
        }
    }
}