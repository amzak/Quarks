namespace Codestellation.Quarks.Testing
{
    public static partial class Some
    {
        public static byte[] Bytes(int size)
        {
            var data = new byte[size];
            Random.NextBytes(data);
            return data;
        }
    }
}