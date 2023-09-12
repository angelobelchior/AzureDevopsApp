using System.Globalization;

namespace AzureDevops.Views.Converters;

public partial class ProjectIdToColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var colors = new[]
        {
            "#FF0000",
            "#AA0000",
            "#5C2893",
            "#DA3A00",
            "#32105C",
            "#0075DA",
            "#5C2893",
            "#DA3A00",
            "#B600A0",
            "#004C1A",
            "#0067B5"
        };

        var index = 0;
        if (value is Guid id)
            index = GetIndexFromGuid(id);
        return Color.FromArgb(colors[index]);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value;

    private static int GetIndexFromGuid(Guid id)
    {
        var text = id.ToString();
        var numbers = RegexPatterns.AnyNumberPattern().Match(text).Value;

        var index = Sum(numbers);
        while (index > 10)
            index = Sum(index.ToString());

        return index;

        int Sum(string values) => values.Sum(number => int.Parse(number.ToString()));
    }
}