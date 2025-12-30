namespace ErrorHandling.Helpers;

/// <summary>
/// Helper class for formatting error codes.
/// </summary>
public static class ErrorCodeHelper
{
    /// <summary>
    /// Formats an error code by replacing thousand separators (commas) with underscores.
    /// </summary>
    /// <param name="code">The error code to format.</param>
    /// <returns>A formatted error code string (e.g., 1000 becomes "1_000").</returns>
    public static string Format(int code)
    {
        return code.ToString("n0").Replace(",", "_");
    }
}