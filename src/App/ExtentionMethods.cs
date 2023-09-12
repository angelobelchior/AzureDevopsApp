namespace AzureDevops;

public static class ExtentionMethods
{
    public static string ToText(this DateTime dateTime, DateTime? nowToTest = null)
    {
        if (dateTime == DateTime.MinValue)
            return "-";

        var now = nowToTest ?? DateTime.UtcNow;
        var diff = now - dateTime;

        return diff switch
        {
            { TotalMinutes: < 60 } => $"{Math.Round(diff.TotalMinutes)}min Ago",
            { TotalHours: < 24 } => $"{Math.Round(diff.TotalHours)}h Ago",
            { TotalDays: < 6 } => dateTime.DayOfWeek.ToString(),
            { TotalDays: < 31 } => $"{Math.Round(diff.TotalDays)}d Ago",
            _ => $"{Math.Round(diff.TotalDays / 365)}y Ago"
        };
    }

    public static string ToText(this TimeSpan timeSpan)
        => timeSpan.TotalSeconds switch
        {
            <= 0 => "-",
            < 60 => $"{Math.Round(timeSpan.TotalSeconds)}s",
            _ => timeSpan.TotalMinutes < 60
                ? $"{Math.Round(timeSpan.TotalMinutes)}m {timeSpan.Seconds}s"
                : $"{Math.Round(timeSpan.TotalHours)}h {timeSpan.Minutes}m {timeSpan.Seconds}s"
        };
}