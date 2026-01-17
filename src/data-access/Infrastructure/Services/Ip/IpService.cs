using System.Net;
using Application.Services.Ip;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services.Ip;

/// <summary>
///     Service for retrieving client IP addresses from HTTP context.
/// </summary>
public class IpService : IIpService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<IpService> _logger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="IpService" /> class.
    /// </summary>
    /// <param name="httpContextAccessor">The HTTP context accessor for retrieving request information.</param>
    /// <param name="logger">The logger for logging warnings and errors.</param>
    public IpService(IHttpContextAccessor httpContextAccessor, ILogger<IpService> logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    /// <summary>
    ///     Gets the raw remote IP address as a string from the current HTTP context.
    /// </summary>
    /// <returns>The remote IP address as a string, or null if unavailable.</returns>
    public string? GetRawRemoteIpAddress()
    {
        var ip = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
        return ip;
    }

    /// <summary>
    ///     Gets the remote IP address as an IPAddress object from the current HTTP context.
    /// </summary>
    /// <returns>The parsed IPAddress object.</returns>
    /// <exception cref="Exception">Thrown when the IP address is invalid or cannot be parsed.</exception>
    public IPAddress GetRemoteIpAddress()
    {
        var ip = GetRawRemoteIpAddress();
        IPAddress? ipAddress = null;
        var isValidIp = !string.IsNullOrWhiteSpace(ip) && IPAddress.TryParse(ip, out ipAddress);

        if (isValidIp && ipAddress is not null) return ipAddress;

        _logger.LogWarning("IP_SERVICE.WARNING.DETAIL --- Invalid ip address {ip}", ip);
        throw new Exception(ip);
    }
}