using FluentValidation.Results;

namespace Application.Common.Validators;

/// <summary>
/// Defines a contract for request validation using FluentValidation.
/// </summary>
/// <typeparam name="TRequest">The type of request to validate.</typeparam>
public interface IRequestValidator<in TRequest>
{
    /// <summary>
    /// Validates a request asynchronously.
    /// </summary>
    /// <param name="request">The request to validate.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A list of validation failures, or an empty list if validation succeeds.</returns>
    Task<List<ValidationFailure>> ValidateAsync(TRequest request, CancellationToken cancellationToken);
}