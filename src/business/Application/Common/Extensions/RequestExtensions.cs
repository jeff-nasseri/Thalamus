using System.Collections.Concurrent;
using System.Reflection;
using ErrorHandling.Attributes;
using ErrorHandling.Enums;
using MediatR;

namespace Application.Common.Extensions;

/// <summary>
/// Provides extension methods for MediatR requests to retrieve handler codes.
/// </summary>
public static class RequestExtensions
{
    private static readonly ConcurrentDictionary<Type, HandlerCode> _handlerCodeCache = new();

    /// <summary>
    /// Gets the handler code from a request instance.
    /// </summary>
    /// <typeparam name="TRequest">The request type implementing <see cref="IBaseRequest"/>.</typeparam>
    /// <param name="request">The request instance.</param>
    /// <returns>The handler code associated with the request type.</returns>
    public static HandlerCode GetHandlerCode<TRequest>(this TRequest request)
        where TRequest : IBaseRequest
    {
        return GetHandlerCodeOfType(request.GetType());
    }

    /// <summary>
    /// Gets the handler code from a request type.
    /// </summary>
    /// <typeparam name="TRequest">The request type implementing <see cref="IBaseRequest"/>.</typeparam>
    /// <returns>The handler code associated with the request type.</returns>
    public static HandlerCode GetHandlerCode<TRequest>()
        where TRequest : IBaseRequest
    {
        return GetHandlerCodeOfType(typeof(TRequest));
    }

    /// <summary>
    /// Gets the handler code from a type, using a cache for performance.
    /// </summary>
    /// <param name="type">The type to get the handler code from.</param>
    /// <returns>The handler code from the HandlerCodeAttribute.</returns>
    private static HandlerCode GetHandlerCodeOfType(Type type)
    {
        return _handlerCodeCache.GetOrAdd(type, t => t.GetCustomAttribute<HandlerCodeAttribute>()!.HandlerCode);
    }
}