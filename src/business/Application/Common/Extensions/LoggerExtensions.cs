using Application.Common.Helper;
using Application.Common.Util;
using ErrorHandling.Enums;
using ErrorHandling.Helpers;
using Microsoft.Extensions.Logging;

namespace Application.Common.Extensions;

/// <summary>
/// Provides extension methods for structured error logging with contextual tags.
/// </summary>
public static class LoggerExtensions
{
    /// <summary>
    /// Logs an error with custom tags and an optional exception.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="exception">The exception to log, if any.</param>
    /// <param name="tags">Dictionary of tags to include in the log scope.</param>
    /// <param name="message">The log message.</param>
    /// <param name="args">Optional message formatting arguments.</param>
    public static void LogError(this ILogger logger, Exception? exception, IDictionary<string, object> tags, string? message,
        params object?[] args)
    {
        using IDisposable? scope = logger.BeginScope(tags);
        logger.Log(LogLevel.Error, exception, message, args);
        scope?.Dispose();
    }

    /// <summary>
    /// Logs an error with error code metadata.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="error">The error code enum.</param>
    /// <param name="message">The log message.</param>
    /// <param name="args">Optional message formatting arguments.</param>
    public static void LogError(this ILogger logger, Enum error, string? message, params object?[] args)
    {
        (BackendErrorType Type, IEnumerable<ServerErrorGroup> Groups, IEnumerable<ErrorResponsibilityTeam> Teams) detail =
            ErrorTypeHelper.ModifyError(error);

        IDictionary<string, object> tags = LoggerUtils.GenerateRequiredErrorLogTags(error, detail.Type, detail.Teams, detail.Groups);

        using IDisposable? scope = logger.BeginScope(tags);
        logger.Log(LogLevel.Error, message, args);
        scope?.Dispose();
    }

    /// <summary>
    /// Logs an error with error code metadata and handler state context.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="this">The handler instance for state extraction.</param>
    /// <param name="error">The error code enum.</param>
    /// <param name="message">The log message.</param>
    /// <param name="args">Optional message formatting arguments.</param>
    public static void LogError(this ILogger logger, object @this, Enum error, string? message, params object?[] args)
    {
        (BackendErrorType Type, IEnumerable<ServerErrorGroup> Groups, IEnumerable<ErrorResponsibilityTeam> Teams) detail =
            ErrorTypeHelper.ModifyError(error);

        message += " --- State : {State}";
        args = args.Append(HandlerStateHelper.GenerateHandlerState(@this)).ToArray();

        IDictionary<string, object> tags = LoggerUtils.GenerateRequiredErrorLogTags(error, detail.Type, detail.Teams, detail.Groups);

        using IDisposable? scope = logger.BeginScope(tags);
        logger.Log(LogLevel.Error, message, args);
        scope?.Dispose();
    }

    /// <summary>
    /// Logs an error with an exception and error code metadata.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="exception">The exception to log.</param>
    /// <param name="error">The error code enum.</param>
    /// <param name="message">The log message.</param>
    /// <param name="args">Optional message formatting arguments.</param>
    public static void LogError(this ILogger logger, Exception? exception, Enum error, string? message, params object?[] args)
    {
        (BackendErrorType Type, IEnumerable<ServerErrorGroup> Groups, IEnumerable<ErrorResponsibilityTeam> Teams) detail =
            ErrorTypeHelper.ModifyError(error);

        IDictionary<string, object> tags = LoggerUtils.GenerateRequiredErrorLogTags(error, detail.Type, detail.Teams, detail.Groups);

        using IDisposable? scope = logger.BeginScope(tags);
        logger.Log(LogLevel.Error, exception, message, args);
        scope?.Dispose();
    }

    /// <summary>
    /// Logs an error with an exception, error code metadata, and handler state context.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="exception">The exception to log.</param>
    /// <param name="this">The handler instance for state extraction.</param>
    /// <param name="error">The error code enum.</param>
    /// <param name="message">The log message.</param>
    /// <param name="args">Optional message formatting arguments.</param>
    public static void LogError(this ILogger logger, Exception? exception, object @this, Enum error, string? message, params object?[] args)
    {
        (BackendErrorType Type, IEnumerable<ServerErrorGroup> Groups, IEnumerable<ErrorResponsibilityTeam> Teams) detail =
            ErrorTypeHelper.ModifyError(error);

        message += " --- State : {State}";
        args = args.Append(HandlerStateHelper.GenerateHandlerState(@this)).ToArray();

        IDictionary<string, object> tags = LoggerUtils.GenerateRequiredErrorLogTags(error, detail.Type, detail.Teams, detail.Groups);

        using IDisposable? scope = logger.BeginScope(tags);
        logger.Log(LogLevel.Error, exception, message, args);
        scope?.Dispose();
    }
}