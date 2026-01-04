namespace Application.Common.Util;

/// <summary>
///     Provides utility methods for URL validation and operations.
/// </summary>
public abstract class UrlUtility
{
    /// <summary>
    ///     Validates whether a string is a valid HTTP or HTTPS URL.
    /// </summary>
    /// <param name="url">The URL string to validate.</param>
    /// <returns>True if the URL is valid with HTTP or HTTPS scheme; otherwise, false.</returns>
    public static bool IsValid(string url)
    {
        return !string.IsNullOrEmpty(url) &&
               Uri.TryCreate(url, UriKind.Absolute, out var uri) &&
               (uri.Scheme == Uri.UriSchemeHttp ||
                uri.Scheme == Uri.UriSchemeHttps);
    }

    /// <summary>
    ///     Validates whether a hostname is valid.
    /// </summary>
    /// <param name="hostname">The hostname to validate.</param>
    /// <returns>True if the hostname is valid; otherwise, false.</returns>
    public static bool IsHostNameValid(string hostname)
    {
        return Uri.CheckHostName(hostname) is not UriHostNameType.Unknown;
    }
}