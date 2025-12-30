using System.Text.Json.Serialization;
using ErrorHandling.Enums;
using ErrorHandling.Helpers;

namespace ErrorHandling;

/// <summary>
/// Represents an error with a code, type, and optional placeholder values.
/// This structure encapsulates error information for both backend and client-side error handling.
/// </summary>
public readonly record struct Error
{
    private Error(int code, Enum errorEnum, BackendErrorType backendType, Dictionary<string, string>? values = null)
    {
        Code = code;
        BackendType = backendType;
        ErrorEnum = errorEnum;
        Values = values;
    }

    private Error(Enum errorEnum, BackendErrorType backendType, Dictionary<string, string>? values = null)
    {
        Code = Convert.ToInt32(errorEnum);
        BackendType = backendType;
        ErrorEnum = errorEnum;
        Values = values;
    }

    /// <summary>
    /// Gets the unique error code.
    /// </summary>
    public int Code { get; }

    /// <summary>
    /// Gets <see cref="BackendErrorType" /> of the error.
    /// </summary>
    [JsonIgnore]
    public BackendErrorType BackendType { get; }

    /// <summary>
    /// Gets <see cref="ClientErrorType" /> of the error.
    /// </summary>
    public ClientErrorType Type => ErrorTypeHelper.GetClientErrorType(BackendType);

    /// <summary>
    /// Gets values of placeholders in the error message.
    /// </summary>
    public Dictionary<string, string>? Values { get; }

    /// <summary>
    /// Gets enum type of the error code.
    /// </summary>
    [JsonIgnore]
    public Enum ErrorEnum { get; }

    /// <summary>
    /// Creates an error from the specified error code enum.
    /// </summary>
    /// <param name="code">The error code enum value.</param>
    /// <param name="values">Optional dictionary of placeholder values for the error message.</param>
    /// <returns>A new <see cref="Error"/> instance.</returns>
    public static Error FromErrorCode(Enum code, Dictionary<string, string>? values = null)
    {
        BackendErrorType backendErrorType = ErrorTypeHelper.GetBackendErrorType(code);
        return new Error(code, backendErrorType, values ?? new Dictionary<string, string>());
    }

    /// <summary>
    /// Creates an error from the specified handler code and error code.
    /// </summary>
    /// <param name="handlerCode">The handler code to prefix the error code.</param>
    /// <param name="code">The error code enum value.</param>
    /// <param name="values">Optional dictionary of placeholder values for the error message.</param>
    /// <returns>A new <see cref="Error"/> instance with a composite code.</returns>
    public static Error FromErrorCode(HandlerCode handlerCode, Enum code,
        Dictionary<string, string>? values = null)
    {
        BackendErrorType backendErrorType = ErrorTypeHelper.GetBackendErrorType(code);
        return new Error((Convert.ToInt32(handlerCode) * 1000) + Convert.ToInt32(code), code, backendErrorType,
            values ?? new Dictionary<string, string>());
    }
}