using Application.Common.Consts;
using Microsoft.AspNetCore.Http;
using Sentry;
using Sentry.Extensibility;

namespace Thalamus.Web.Logging.Sentry;

/// <summary>
///     Custom Sentry event processor that enriches events with error metadata and user information.
/// </summary>
public class CustomSentryEventProcessor : ISentryEventProcessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    ///     Initializes a new instance of the <see cref="CustomSentryEventProcessor" /> class.
    /// </summary>
    /// <param name="httpContextAccessor">HTTP context accessor for retrieving request information.</param>
    public CustomSentryEventProcessor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    ///     Processes a Sentry event by enriching it with custom tags and user information.
    /// </summary>
    /// <param name="event">The Sentry event to process.</param>
    /// <returns>The enriched Sentry event.</returns>
    public SentryEvent Process(SentryEvent @event)
    {
        var ipAddress = _httpContextAccessor?.HttpContext?.Connection.RemoteIpAddress?.ToString();

        SetResponsibleTeamsTag(ref @event);
        SetErrorGroupsTag(ref @event);
        SetErrorTypeTag(ref @event);

        SetErrorDescriptionTag(ref @event);
        SetErrorCodeTag(ref @event);
        SetErrorTag(ref @event);

        SetPofDescriptionTag(ref @event);
        SetPofCodeTag(ref @event);
        SetPofTag(ref @event);

        @event.User = new User
        {
            IpAddress = ipAddress
        };

        return @event;
    }

    /// <summary>
    ///     Sets the point of failure description tag on the Sentry event.
    /// </summary>
    /// <param name="event">The Sentry event to modify.</param>
    private void SetPofDescriptionTag(ref SentryEvent @event)
    {
        KeyValuePair<string, object?> pofDescription =
            @event.Extra.SingleOrDefault(e => e.Key == ErrorLoggerKeys.POF_EXPLANATION);

        if (pofDescription.Value is not null)
            @event.SetTag(ErrorLoggerKeys.POF_EXPLANATION, pofDescription.Value.ToString()!);
    }

    /// <summary>
    ///     Sets the point of failure code tag on the Sentry event.
    /// </summary>
    /// <param name="event">The Sentry event to modify.</param>
    private void SetPofCodeTag(ref SentryEvent @event)
    {
        KeyValuePair<string, object?> pofCode = @event.Extra.SingleOrDefault(e => e.Key == ErrorLoggerKeys.POF_CODE);

        if (pofCode.Value is not null) @event.SetTag(ErrorLoggerKeys.POF_CODE, pofCode.Value.ToString()!);
    }

    /// <summary>
    ///     Sets the point of failure tag on the Sentry event.
    /// </summary>
    /// <param name="event">The Sentry event to modify.</param>
    private void SetPofTag(ref SentryEvent @event)
    {
        KeyValuePair<string, object?> pof = @event.Extra.SingleOrDefault(e => e.Key == ErrorLoggerKeys.POF);

        if (pof.Value is not null) @event.SetTag(ErrorLoggerKeys.POF, pof.Value!.ToString()!);
    }

    /// <summary>
    ///     Sets the error description tag on the Sentry event.
    /// </summary>
    /// <param name="event">The Sentry event to modify.</param>
    private void SetErrorDescriptionTag(ref SentryEvent @event)
    {
        KeyValuePair<string, object?> errorDescription =
            @event.Extra.SingleOrDefault(e => e.Key == ErrorLoggerKeys.ERROR_EXPLANATION);

        if (errorDescription.Value is not null)
            @event.SetTag(ErrorLoggerKeys.ERROR_EXPLANATION, errorDescription.Value.ToString()!);
    }

    /// <summary>
    ///     Sets the error code tag on the Sentry event.
    /// </summary>
    /// <param name="event">The Sentry event to modify.</param>
    private void SetErrorCodeTag(ref SentryEvent @event)
    {
        KeyValuePair<string, object?>
            errorCode = @event.Extra.SingleOrDefault(e => e.Key == ErrorLoggerKeys.ERROR_CODE);

        if (errorCode.Value is not null) @event.SetTag(ErrorLoggerKeys.ERROR_CODE, errorCode.Value.ToString()!);
    }

    /// <summary>
    ///     Sets the error tag on the Sentry event.
    /// </summary>
    /// <param name="event">The Sentry event to modify.</param>
    private void SetErrorTag(ref SentryEvent @event)
    {
        KeyValuePair<string, object?> error = @event.Extra.SingleOrDefault(e => e.Key == ErrorLoggerKeys.ERROR);

        if (error.Value is not null) @event.SetTag(ErrorLoggerKeys.ERROR, error.Value.ToString()!);
    }

    /// <summary>
    ///     Sets the error type tag on the Sentry event.
    /// </summary>
    /// <param name="event">The Sentry event to modify.</param>
    private void SetErrorTypeTag(ref SentryEvent @event)
    {
        KeyValuePair<string, object?> type = @event.Extra.SingleOrDefault(e => e.Key == ErrorLoggerKeys.ERROR_TYPE);

        if (type.Value is not null) @event.SetTag(ErrorLoggerKeys.ERROR_TYPE, type.Value!.ToString()!);
    }

    /// <summary>
    ///     Sets the error groups tag on the Sentry event.
    /// </summary>
    /// <param name="event">The Sentry event to modify.</param>
    private void SetErrorGroupsTag(ref SentryEvent @event)
    {
        KeyValuePair<string, object?> groups = @event.Extra.SingleOrDefault(e => e.Key == ErrorLoggerKeys.ERROR_GROUPS);

        if (groups.Value is not null) @event.SetTag(ErrorLoggerKeys.ERROR_GROUPS, groups.Value.ToString()!);
    }

    /// <summary>
    ///     Sets the responsible teams tag on the Sentry event.
    /// </summary>
    /// <param name="event">The Sentry event to modify.</param>
    private void SetResponsibleTeamsTag(ref SentryEvent @event)
    {
        KeyValuePair<string, object?> teams =
            @event.Extra.SingleOrDefault(e => e.Key == ErrorLoggerKeys.RESPONSIBLE_TEAMS);

        if (teams.Value is not null) @event.SetTag(ErrorLoggerKeys.RESPONSIBLE_TEAMS, teams.Value.ToString()!);
    }
}