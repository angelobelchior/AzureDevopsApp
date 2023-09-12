using System.Globalization;
using AzureDevops.Client.Models;

namespace AzureDevops.Views.Converters;

public class ProjectVisibilityToIconConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is ProjectVisibility v && v == ProjectVisibility.Private ? "\uf023" : string.Empty;

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value;
}