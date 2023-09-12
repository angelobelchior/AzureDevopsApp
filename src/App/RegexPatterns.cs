using System.Text.RegularExpressions;

namespace AzureDevops;

public partial class RegexPatterns
{
    [GeneratedRegex(@"\d+")]
    public static partial Regex AnyNumberPattern();
}