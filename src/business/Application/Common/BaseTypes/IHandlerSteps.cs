using ErrorHandling;
using MediatR;

namespace Application.Common.BaseTypes;

/// <summary>
///     Marker interface for defining handler steps in a multi-step handler process.
///     Implement this interface to indicate all related steps for the handler.
/// </summary>
public interface IHandlerSteps
{
}

/// <summary>
///     Defines handler steps for a specific request and response type.
/// </summary>
/// <typeparam name="TRequest">The type of the request implementing <see cref="IRequest{TResponse}" />.</typeparam>
/// <typeparam name="TResponse">The type of the response implementing <see cref="IResponse" />.</typeparam>
public interface IHandlerSteps<in TRequest, TResponse> : IHandlerSteps
    where TResponse : IResponse
    where TRequest : IRequest<TResponse>
{
    /// <summary>
    ///     Prepares the initial state for the handler with the given request.
    /// </summary>
    /// <param name="request">The request object to process.</param>
    /// <param name="cancellationToken">Optional cancellation token to cancel the operation.</param>
    void PrepaidState(TRequest request, CancellationToken cancellationToken = default);
}

/// <summary>
///     Defines handler steps for a specific request, response, and data type.
/// </summary>
/// <typeparam name="TRequest">The type of the request implementing <see cref="IRequest{TResponse}" />.</typeparam>
/// <typeparam name="TResponse">The type of the response implementing <see cref="IResponse{T}" />.</typeparam>
/// <typeparam name="T">The type of data contained in the response.</typeparam>
public interface IHandlerSteps<in TRequest, TResponse, T> : IHandlerSteps
    where TResponse : IResponse<T>
    where TRequest : IRequest<TResponse>
{
    /// <summary>
    ///     Prepares the initial state for the handler with the given request.
    /// </summary>
    /// <param name="request">The request object to process.</param>
    /// <param name="cancellationToken">Optional cancellation token to cancel the operation.</param>
    void PrepaidState(TRequest request, CancellationToken cancellationToken = default);
}