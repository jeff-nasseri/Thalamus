using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Thalamus.Web.Middlewares;

/// <summary>
/// Middleware for logging authentication and authorization errors.
/// </summary>
public class ErrorLoggingMiddleware
{
    private readonly ILogger _logger;
    private readonly RequestDelegate _next;

    /// <summary>
    /// Initializes a new instance of the <see cref="ErrorLoggingMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next middleware in the pipeline.</param>
    /// <param name="logger">Logger instance for recording errors.</param>
    public ErrorLoggingMiddleware(RequestDelegate next, ILogger<ErrorLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// Invokes the middleware to process the HTTP request and log authorization errors.
    /// </summary>
    /// <param name="httpContext">The HTTP context for the current request.</param>
    public async Task InvokeAsync(HttpContext httpContext)
    {
        await _next(httpContext);

        switch (httpContext.Response.StatusCode)
        {
            case 401:
            case 403:
                string ipAddress = "NAN";
                string? authorizationHeader = httpContext.Request.Headers["Authorization"];

                _logger.LogWarning(
                    "HTTP status code '{StatusCode}' returned for authorization header '{AuthorizationHeader}' sent from '{RemoteIpAddress}'",
                    httpContext.Response.StatusCode,
                    authorizationHeader,
                    ipAddress);
                break;
        }
    }
}