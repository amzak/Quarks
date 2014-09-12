using System;
using System.IO;
using System.Linq;

namespace Codestellation.Quarks.IO
{
    internal static class Folder
    {
        public static readonly string BasePath = AppDomain.CurrentDomain.BaseDirectory;

        public static string Combine(params string[] folders)
        {
            var combined = Path.Combine(folders);

            return ToFullPath(combined);
        }

        public static void EnsureExists(string folder)
        {
            var fullPath = ToFullPath(folder);
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }
        }

        public static void EnsureDeleted(string folder)
        {
            var fullPath = ToFullPath(folder);
            if (Directory.Exists(fullPath))
            {
                Directory.Delete(fullPath, true);
            }
        }

        public static FileInfo[] EnumerateFiles(string folder)
        {
            var fullPath = ToFullPath(folder);
            return Directory.EnumerateFiles(fullPath)
                .Select(x => new FileInfo(x))
                .ToArray();
        }

        public static bool Exists(string folder)
        {
            var fullPath = ToFullPath(folder);
            return Directory.Exists(fullPath);
        }

        public static string ToFullPath(string path)
        {
            return Path.Combine(BasePath, path);
        }
    }
}