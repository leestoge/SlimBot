
namespace SlimBot.Utilities
{
    internal static class TextFormatting
    {
        public static string RemoveExcessCharacters(this string value, int maxLen)
        {
            return (value.Length > maxLen) ? value.Substring(0, maxLen) : value;
        }
    }
}
