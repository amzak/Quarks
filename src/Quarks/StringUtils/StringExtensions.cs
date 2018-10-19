namespace Codestellation.Quarks.StringUtils
{
    public static class StringExtensions
    {
        public static string FormatWith(this string self, params object[] args)
        {
            return string.Format(self, args);
        }
    }
}