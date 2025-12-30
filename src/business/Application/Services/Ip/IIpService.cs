using System.Net;

namespace Application.Services.Ip;

/// <summary>
/// Provides services for retrieving client IP addresses from HTTP requests.
/// </summary>
public interface IIpService
{
    /// <summary>
    /// Gets the raw remote IP address as a string.
    /// </summary>
    /// <returns>The IP address as a string, or null if not available.</returns>
    string? GetRawRemoteIpAddress();

    /// <summary>
    /// Gets the remote IP address as an IPAddress object.
    /// </summary>
    /// <returns>The IPAddress of the remote client.</returns>
    IPAddress GetRemoteIpAddress();
}