using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Thalamus.Web.Swagger;

/// <summary>
///     Middleware that provides basic authentication for Swagger endpoints.
/// </summary>
public class SwaggerBasicAuthMiddleware
{
    private readonly string? _expectedPassword;
    private readonly string? _expectedUsername;
    private readonly RequestDelegate _next;

    /// <summary>
    ///     Initializes a new instance of the <see cref="SwaggerBasicAuthMiddleware" /> class.
    /// </summary>
    /// <param name="next">The next middleware in the pipeline.</param>
    /// <param name="expectedUsername">Expected username for authentication.</param>
    /// <param name="expectedPassword">Expected password for authentication.</param>
    public SwaggerBasicAuthMiddleware(RequestDelegate next, string expectedUsername, string expectedPassword)
    {
        _next = next;
        _expectedUsername = expectedUsername;
        _expectedPassword = expectedPassword;
    }

    /// <summary>
    ///     Invokes the middleware to validate basic authentication credentials.
    /// </summary>
    /// <param name="context">The HTTP context for the current request.</param>
    public async Task InvokeAsync(HttpContext context)
    {
        string? authHeader = context.Request.Headers["Authorization"];

        if (authHeader != null && authHeader.StartsWith("Basic "))
        {
            // Get the credentials from request header
            var header = AuthenticationHeaderValue.Parse(authHeader);
            var inBytes = Convert.FromBase64String(header.Parameter!);
            string[] credentials = Encoding.UTF8.GetString(inBytes).Split(':');
            var username = credentials[0];
            var password = credentials[1];

            // validate credentials
            if (username.Equals(_expectedUsername) && password.Equals(_expectedPassword))
            {
                await _next.Invoke(context).ConfigureAwait(false);
                return;
            }
        }

        context.Response.Headers["WWW-Authenticate"] = "Basic";
        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
    }
}