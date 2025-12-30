using Application.Common.Validators;
using FluentValidation;
using FluentValidation.Results;

namespace Infrastructure.Validators;

/// <summary>
/// Generic request validator that aggregates and executes multiple FluentValidation validators.
/// </summary>
/// <typeparam name="TRequest">The type of request to validate.</typeparam>
public class RequestValidator<TRequest> : IRequestValidator<TRequest>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    /// <summary>
    /// Initializes a new instance of the <see cref="RequestValidator{TRequest}"/> class.
    /// </summary>
    /// <param name="validators">The collection of validators to execute.</param>
    public RequestValidator(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    /// <summary>
    /// Validates the specified request asynchronously using all registered validators.
    /// </summary>
    /// <param name="request">The request to validate.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of validation failures from all validators.</returns>
    public async Task<List<ValidationFailure>> ValidateAsync(TRequest request, CancellationToken cancellationToken)
    {
        ValidationContext<TRequest> context = new(request);

        ValidationResult[] validationResults = await Task.WhenAll(
            _validators.Select(v =>
                v.ValidateAsync(context, cancellationToken)));

        return validationResults
            .Where(r => r.Errors.Any())
            .SelectMany(r => r.Errors)
            .ToList();
    }
}