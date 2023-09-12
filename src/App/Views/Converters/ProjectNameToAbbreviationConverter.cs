using System.Globalization;

namespace AzureDevops.Views.Converters;

public class ProjectNameToAbbreviationConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string name && !string.IsNullOrEmpty(name))
        {
            var parts = name.Split(' ');
            if (parts.Length == 1)
            {
                var abbreviation = string.Empty;
                foreach (var c in name)
                {
                    if (char.IsUpper(c))
                        abbreviation += c.ToString();
                    if (abbreviation.Length == 2) break;
                }

                if (string.IsNullOrEmpty(abbreviation) || abbreviation.Length == 1)
                    abbreviation = name.Substring(0, 2);

                return abbreviation;
            }

            return $"{parts[0][0]}{parts[1][0]}";
        }

        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value;
}