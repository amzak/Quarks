using System;
using System.Reflection;

namespace Codestellation.Quarks
{
    public static class RandomExtenstion
    {
        // full framework, netcore
        private static readonly FieldInfo inextFieldInfo = GetField("_inext") ?? GetField("inext");
        private static readonly FieldInfo inextpFieldInfo = GetField("_inextp") ?? GetField("inextp");
        private static readonly FieldInfo seedArrayFieldInfo = GetField("_seedArray") ?? GetField("SeedArray");

        public static byte[] GetState(this Random self)
        {
            var buffer = new byte[232]; // (4 + 4 + 56) * 4

            var inext = (int)inextFieldInfo.GetValue(self);
            Write(inext, buffer, 0);

            var inextp = (int)inextpFieldInfo.GetValue(self);
            Write(inextp, buffer, 4);

            var seedArray = (int[])seedArrayFieldInfo.GetValue(self);
            for (var i = 0; i < seedArray.Length; i++)
            {
                Write(seedArray[i], buffer, 8 + i * 4);
            }

            return buffer;
        }

        public static void RestoreState(this Random self, byte[] state)
        {
            int inext = Read(state, 0);
            inextFieldInfo.SetValue(self, inext);

            int inextp = Read(state, 4);
            inextpFieldInfo.SetValue(self, inextp);

            var seedArray = (int[])seedArrayFieldInfo.GetValue(self);
            for (var i = 0; i < 56; i++)
            {
                seedArray[i] = Read(state, 8 + i * 4);
            }
        }

        private static FieldInfo GetField(string name) => typeof(Random).GetField(name, BindingFlags.Instance | BindingFlags.NonPublic);

        private static void Write(int value, byte[] buffer, int offset)
        {
            buffer[offset + 0] = (byte)value;
            buffer[offset + 1] = (byte)(value >> 8);
            buffer[offset + 2] = (byte)(value >> 16);
            buffer[offset + 3] = (byte)(value >> 24);
        }

        private static int Read(byte[] buffer, int offset) =>
            buffer[offset + 0] |
            (buffer[offset + 1] << 8) |
            (buffer[offset + 2] << 16) |
            (buffer[offset + 3] << 24);
    }
}