using Application.Common.Consts;
using Application.Common.ErrorMessaging;
using Application.Common.Extensions;
using Application.Common.Validators;
using Application.Services.Ip;
using ErrorHandling;
using ErrorHandling.Enums;
using ErrorHandling.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Common.PipelineBehaviors;

/// <summary>
///     MediatR pipeline behavior that handles common error scenarios including validation and logging.
/// </summary>
/// <typeparam name="TRequest">The request type implementing <see cref="IRequest{TResponse}" />.</typeparam>
/// <typeparam name="TResponse">The response type implementing <see cref="IResponse" />.</typeparam>
public class CommonErrorsPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : struct, IResponse
{
    private readonly IIpService _ipService;
    private readonly ILogger<CommonErrorsPipelineBehavior<TRequest, TResponse>> _logger;
    private readonly IRequestValidator<TRequest> _requestValidator;

    /// <summary>
    ///     Initializes a new instance of the <see cref="CommonErrorsPipelineBehavior{TRequest, TResponse}" /> class.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="requestValidator">The request validator.</param>
    /// <param name="ipService">The IP address service.</param>
    public CommonErrorsPipelineBehavior(
        ILogger<CommonErrorsPipelineBehavior<TRequest, TResponse>> logger,
        IRequestValidator<TRequest> requestValidator,
        IIpService ipService)
    {
        _logger = logger;
        _requestValidator = requestValidator;
        _ipService = ipService;
    }

    /// <summary>
    ///     Handles the request by performing validation, logging, and error enrichment.
    /// </summary>
    /// <param name="request">The request to handle.</param>
    /// <param name="next">The next handler in the pipeline.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The response from the handler.</returns>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var handlerCode = request.GetHandlerCode();
        var handlerNumber = ErrorCodeHelper.Format((int)handlerCode);

        LogRequestStarting(handlerCode, handlerNumber);
        _logger.LogDebug("HANDLER.REQUEST.DETAILS --- {HandlerCode} ({HandlerName}) --- {Request}", handlerNumber,
            handlerCode, request);

        var errorList = await _requestValidator.ValidateAsync(request, cancellationToken);

        TResponse response;

        if (errorList.Any())
        {
            var error = CommonErrorCode.ValidationFailed;
            response = new TResponse
            {
                Success = false,
                Error = error.ForRequest(
                    request,
                    errorList.GetRange(0, 1).ToDictionary(x => x.PropertyName, x => x.ErrorMessage))
            };

            _logger.LogError(
                error,
                "HANDLER.ERROR.VALIDATION --- {HandlerCode} ({HandlerName}) --- ErrorCode: {ErrorCode}\n    {@Errors}",
                handlerCode,
                handlerNumber,
                ErrorCodeHelper.Format(Convert.ToInt32(response.Error!.Value.ErrorEnum)),
                errorList);

            return response;
        }

        response = await next();

        if (response.Success)
        {
            _logger.LogInformation(
                "HANDLER.RESPONSE.SUCCESS --- {HandlerCode} ({HandlerName})",
                handlerNumber,
                handlerCode);
        }
        else
        {
            var code = response.Error!.Value.ErrorEnum;
            response.Error.Value.Values!.Add(ApplicationKeys.ERROR_SERVER_MAIN_MESSAGE,
                ErrorMessageUtils.GetErrorMessage(code));
        }

        return response;
    }

    /// <summary>
    ///     Logs the start of a request with handler code and client IP address.
    /// </summary>
    /// <param name="handlerCode">The handler code.</param>
    /// <param name="handlerNumber">The formatted handler number.</param>
    private void LogRequestStarting(HandlerCode handlerCode, string handlerNumber)
    {
        var ipAddress = _ipService.GetRawRemoteIpAddress() ?? "Unknown IP";

        _logger.LogInformation(
            "HANDLER.REQUEST.STARTING --- {HandlerCode} ({HandlerName}) --- IP: {Ip}",
            handlerNumber,
            handlerCode,
            ipAddress);
    }
}