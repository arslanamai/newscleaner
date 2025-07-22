using System.Net;

namespace newscleanerconsole.Services;

public static class TextCleaner
{
    public static string CleanText(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return string.Empty;
        string decoded = WebUtility.HtmlDecode(input);
        return decoded.Trim().Replace("\n", "").Replace("\r", "").Replace(" ", " ");
    }
}