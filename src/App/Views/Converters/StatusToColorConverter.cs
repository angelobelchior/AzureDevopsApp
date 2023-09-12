using System.Globalization;
using AzureDevops.Client.Models;

namespace AzureDevops.Views.Converters;

public class StatusToColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var colors = new Dictionary<string, Color>
        {
            ["_default"] = Colors.White,
            ["None"] = Color.FromArgb("#FBFFF7"),
            ["Abandoned"] = Color.FromArgb("#C0C0C0"),
            ["Skipped"] = Color.FromArgb("#96F5E3"),
            ["Failed"] = Color.FromArgb("#FF0000"),
            ["Canceled"] = Color.FromArgb("#DCDCDC"),
            ["Succeeded"] = Color.FromArgb("#0CBC5F"),
            ["SucceededWithIssues"] = Color.FromArgb("#ADFF2F"),
            ["PartiallySucceeded"] = Color.FromArgb("#C1FF33 "),
        };

        return value switch
        {
            BuildResultStatus result => result switch
            {
                BuildResultStatus.None => colors["None"],
                BuildResultStatus.Canceled => colors["Canceled"],
                BuildResultStatus.PartiallySucceeded => colors["PartiallySucceeded"],
                BuildResultStatus.Failed => colors["Failed"],
                BuildResultStatus.Succeeded => colors["Succeeded"],
                _ => colors["_default"]
            },
            TaskResult taskResult => taskResult switch
            {
                TaskResult.Abandoned => colors["Abandoned"],
                TaskResult.Canceled => colors["Canceled"],
                TaskResult.Failed => colors["Failed"],
                TaskResult.Skipped => colors["Skipped"],
                TaskResult.SucceededWithIssues => colors["SucceededWithIssues"],
                TaskResult.Succeeded => colors["Succeeded"],
                _ => colors["_default"]
            },
            _ => value
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value;
}