using ErrorHandling;
using ErrorHandling.Enums;
using MediatR;

namespace Application.Common.Extensions;

/// <summary>
/// Provides extension methods for creating errors from error codes in the context of requests.
/// </summary>
public static class CommonErrorCodeExtensions
{
    /// <summary>
    /// Creates an Error from an error code for a specific request instance.
    /// </summary>
    /// <typeparam name="TRequest">The request type implementing <see cref="IBaseRequest"/>.</typeparam>
    /// <param name="errorCode">The error code enum.</param>
    /// <param name="request">The request instance.</param>
    /// <param name="values">Optional dictionary of additional error context values.</param>
    /// <returns>An Error object associated with the request's handler code.</returns>
    public static Error ForRequest<TRequest>(this Enum errorCode, TRequest request,
        Dictionary<string, string>? values = null)
        where TRequest : IBaseRequest
    {
        return Error.FromErrorCode(request.GetHandlerCode(), errorCode, values);
    }

    /// <summary>
    /// Creates an Error from a common error code for a specific request type.
    /// </summary>
    /// <typeparam name="TRequest">The request type implementing <see cref="IBaseRequest"/>.</typeparam>
    /// <param name="errorCode">The common error code.</param>
    /// <param name="values">Optional dictionary of additional error context values.</param>
    /// <returns>An Error object associated with the request type's handler code.</returns>
    public static Error ForRequestType<TRequest>(this CommonErrorCode errorCode,
        Dictionary<string, string>? values = null)
        where TRequest : IBaseRequest
    {
        return Error.FromErrorCode(RequestExtensions.GetHandlerCode<TRequest>(), errorCode, values);
    }
}