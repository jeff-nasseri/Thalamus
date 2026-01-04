using System.Text.Json;

namespace Application.Common.Helper;

/// <summary>
///     Provides helper methods for generating handler state information for logging and debugging.
/// </summary>
public static class HandlerStateHelper
{
    /// <summary>
    ///     Generates a JSON string representation of all public and private fields and properties of the handler.
    /// </summary>
    /// <typeparam name="THandler">The handler type.</typeparam>
    /// <param name="handler">The handler instance.</param>
    /// <returns>A JSON string representation of the handler state, or an error message if serialization fails.</returns>
    public static string GenerateHandlerState<THandler>(THandler handler)
    {
        try
        {
            var json = JsonSerializer.Serialize(handler);
            return json;
        }
        catch (Exception exception)
        {
            var state = $"""
                         ERROR IN STATE GENERATOR HAPPENED
                         HANDLER TYPE : {typeof(THandler)}
                         ERROR MESSAGE : {exception.Message}
                         POSSIBLE HANDLER REQUEST DATA : {GetHandlerRequestInfo(handler)}
                         STACK TRACE : {exception.StackTrace}
                         """;

            return state;
        }
    }

    /// <summary>
    ///     Attempts to retrieve and serialize the HandlerRequest property from the handler.
    /// </summary>
    /// <typeparam name="THandler">The handler type.</typeparam>
    /// <param name="handler">The handler instance.</param>
    /// <returns>A JSON string of the handler request, or "ERROR" if retrieval or serialization fails.</returns>
    private static string GetHandlerRequestInfo<THandler>(THandler handler)
    {
        var request = typeof(THandler).GetProperty("HandlerRequest");
        var value = request?.GetValue(handler);

        if (request is null || value is null) return "ERROR";

        try
        {
            return JsonSerializer.Serialize(value);
        }
        catch
        {
            return "ERROR";
        }
    }
}