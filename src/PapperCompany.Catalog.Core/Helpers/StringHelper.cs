using System.Text;
using System.Text.RegularExpressions;

namespace PapperCompany.Catalog.Core;

public static class StringHelper
{
    public static string ToCamelCase(this string value)
    {
        if (string.IsNullOrEmpty(value)) return string.Empty;
        string[] words = Regex.Split(value.Trim(), @"[\s\-_\.]+");
        if (words.Length == 0) return string.Empty;

        StringBuilder builder = new();
        builder.Append(words[0].ToLower());

        for (int i = 1; i < words.Length; i++)
        {
            string word = words[i];
            if (word.Length > 0) builder.Append(char.ToUpper(word[0]) + word.Substring(1).ToLower());
        }

        return builder.ToString();
    }

    public static bool ContainsSqlInjection(this string value)
    {
        if (string.IsNullOrEmpty(value)) return true;

        Regex rgx = new (
        @"(\b(ALTER|CREATE|DELETE|DROP|EXEC(UTE)?|INSERT( INTO)?|MERGE|SELECT|UPDATE|UNION( ALL)?)\b)|(--|;|--|\*|\/\*|\*\/|'|%27|%22|%3B|%3D|%2D|=|%23|#|\s+OR\s+|\s+AND\s+)",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);

        return rgx.IsMatch(value);
    }
}
