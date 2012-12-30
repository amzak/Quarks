namespace Codestellation.Common.Extentions
{
    public static class StringExt
    {
        public static string FormatWith(this string self, params object[] args)
        {
            return string.Format(self, args);
        }
    }
}