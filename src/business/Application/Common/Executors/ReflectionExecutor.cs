using System.Reflection;
using Application.Common.Attributes;
using Application.Common.Models.Handler;
using ErrorHandling;
using ErrorHandling.Attributes;
using MediatR;

namespace Application.Common.Executors;

/// <summary>
///     Executes MediatR request handlers using reflection to invoke handler steps in order.
/// </summary>
/// <typeparam name="TRequest">The request type implementing <see cref="IRequest{TResponse}" />.</typeparam>
/// <typeparam name="TResponse">The response type implementing <see cref="IResponse" />.</typeparam>
/// <typeparam name="TRequestHandler">The handler type that processes the request.</typeparam>
/// <remarks>
///     This executor is marked as obsolete due to performance costs associated with reflection invocation.
/// </remarks>
[Obsolete("Cost effect because of reflection invocation")]
public class ReflectionExecutor<TRequest, TResponse, TRequestHandler>
    where TRequest : IRequest<TResponse>
    where TResponse : IResponse
    where TRequestHandler : notnull
{
    /// <summary>
    ///     Executes the handler by invoking all handler steps in their defined order.
    /// </summary>
    /// <param name="handler">The handler instance to execute.</param>
    /// <param name="request">The request to process.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>The response from the handler execution.</returns>
    /// <exception cref="PlatformNotSupportedException">Thrown when the handler does not implement PrepaidState.</exception>
    public static async Task<TResponse> ExecuteAsync(TRequestHandler handler, TRequest request,
        CancellationToken cancellationToken = default)
    {
        var prepaidState = typeof(TRequest).GetMethod("PrepaidState");

        if (prepaidState is null)
            throw new PlatformNotSupportedException($"Your handler {handler} should implement PrepaidState.");

        prepaidState.Invoke(handler, new object[] { request, cancellationToken });

        var steps = typeof(TRequestHandler).GetMethods()
            .Where(m => m.GetCustomAttribute<HandlerStepAttribute>() != null).ToList();

        var handlerStepModels = steps.Select(s => new HandlerStepModel
        {
            Name = s.Name,
            Order = s.GetCustomAttribute<HandlerStepAttribute>()!.Order
        }).ToList();

        handlerStepModels = handlerStepModels.OrderBy(o => o.Order).ToList();

        List<object> responses = new();

        foreach (var stepModel in handlerStepModels)
        {
            var methodInfo = typeof(TRequestHandler).GetMethod(stepModel.Name);

            try
            {
                var task = (Task)methodInfo!.Invoke(handler, null)!;
                await task.ConfigureAwait(false);
                var propertyInfo = task.GetType().GetProperty("Result");
                var result = (IResponse)propertyInfo!.GetValue(task)!;

                if (!result.Success) return (TResponse)result;

                responses.Add(result);
            }
            catch (Exception exception)
            {
                var handleExceptionAttributes = methodInfo!
                    .GetCustomAttributes<HandleExceptionAttribute>().Where(a => a.ExceptionType == exception.GetType());

                foreach (var handleExceptionAttribute in handleExceptionAttributes)
                {
                    var method =
                        handleExceptionAttribute.ExceptionHandlerType.GetMethod(handleExceptionAttribute
                            .ExceptionHandlerMethodName);

                    method!.Invoke(null, new object[] { exception, handler });
                }
            }
        }

        return (TResponse)responses.Last();
    }
}