using ErrorHandling;
using MediatR;

namespace Application.Common.BaseTypes;

/// <summary>
///     Defines the base state for a handler, including cancellation support.
/// </summary>
public interface IHandlerDefaultState
{
    /// <summary>
    ///     Gets or sets the cancellation token for the handler operation.
    /// </summary>
    CancellationToken CancellationToken { get; set; }
}

/// <summary>
///     Defines the default state for a handler with specific request and response types.
/// </summary>
/// <typeparam name="TRequest">The type of the request implementing <see cref="IRequest{TResponse}" />.</typeparam>
/// <typeparam name="TResponse">The type of the response implementing <see cref="IResponse" />.</typeparam>
public interface IHandlerDefaultState<TRequest, TResponse> : IHandlerDefaultState
    where TResponse : IResponse
    where TRequest : IRequest<TResponse>
{
    /// <summary>
    ///     Gets or sets the request object for the handler.
    /// </summary>
    TRequest HandlerRequest { get; set; }
}

/// <summary>
///     Defines the default state for a handler with specific request, response, and data types.
/// </summary>
/// <typeparam name="TRequest">The type of the request implementing <see cref="IRequest{TResponse}" />.</typeparam>
/// <typeparam name="TResponse">The type of the response implementing <see cref="IResponse{T}" />.</typeparam>
/// <typeparam name="T">The type of data contained in the response.</typeparam>
public interface IHandlerDefaultState<TRequest, TResponse, T> : IHandlerDefaultState
    where TResponse : IResponse<T>
    where TRequest : IRequest<TResponse>
{
    /// <summary>
    ///     Gets or sets the request object for the handler.
    /// </summary>
    TRequest HandlerRequest { get; set; }
}