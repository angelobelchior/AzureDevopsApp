using System.Globalization;

namespace AzureDevops.Views.Converters;

public class DepthToColumnDefinitionWidthConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        const int width = 6;
        if (value is int depth)
            return width * (depth * 2);
        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value;
}
