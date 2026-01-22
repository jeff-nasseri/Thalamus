using Application.Common.Validators;
using FluentValidation.Results;

namespace Application.Business.Node.Commands.InitializeNode;

/// <summary>
///     Validator for node initialization requests.
///     Validates the request payload before processing.
/// </summary>
public class InitializeNodeValidator : IRequestValidator<InitializeNodeRequest>
{
    /// <summary>
    ///     Validates the node initialization request asynchronously.
    /// </summary>
    /// <param name="request">The initialization request to validate.</param>
    /// <param name="cancellationToken">Token to cancel the validation operation.</param>
    /// <returns>A list of validation failures, or an empty list if validation succeeds.</returns>
    /// <exception cref="NotImplementedException">This method is not yet implemented.</exception>
    public Task<List<ValidationFailure>> ValidateAsync(InitializeNodeRequest request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}