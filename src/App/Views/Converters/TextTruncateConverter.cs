using System.Globalization;

namespace AzureDevops.Views.Converters;

public class TextTruncateConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not string text) return value;
        if (parameter is null) return value;
        var length = int.Parse(parameter.ToString()!);
        if (string.IsNullOrEmpty(text)) return string.Empty;
        if (length == 0) return text;
        return text.Length <= length ? text : $"{text[..length]}...";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value;
}