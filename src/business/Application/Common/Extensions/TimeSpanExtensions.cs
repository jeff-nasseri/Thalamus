namespace Application.Common.Extensions;

/// <summary>
/// Provides extension methods for TimeSpan formatting.
/// </summary>
public static class TimeSpanExtensions
{
    /// <summary>
    /// Converts a TimeSpan to a human-readable string format.
    /// </summary>
    /// <param name="span">The TimeSpan to convert.</param>
    /// <returns>A human-readable string representation like "2 days, 3 hours, 15 minutes, 30 seconds".</returns>
    public static string ToReadableString(this TimeSpan span)
    {
        string formatted =
            $"{(span.Duration().Days > 0 ? $"{span.Days:0} day{(span.Days == 1 ? string.Empty : "s")}, " : string.Empty)}{(span.Duration().Hours > 0 ? $"{span.Hours:0} hour{(span.Hours == 1 ? string.Empty : "s")}, " : string.Empty)}{(span.Duration().Minutes > 0 ? $"{span.Minutes:0} minute{(span.Minutes == 1 ? string.Empty : "s")}, " : string.Empty)}{(span.Duration().Seconds > 0 ? $"{span.Seconds:0} second{(span.Seconds == 1 ? string.Empty : "s")}" : string.Empty)}";

        if (formatted.EndsWith(", "))
        {
            formatted = formatted[..^2];
        }

        if (string.IsNullOrEmpty(formatted))
        {
            formatted = "0 seconds";
        }

        return formatted;
    }
}