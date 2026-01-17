using Application.Common.Validators;
using FluentValidation.Results;

namespace Application.Business.Node.Commands.InitializeNode;

public class InitializeNodeValidator : IRequestValidator<InitializeNodeRequest>
{
    public Task<List<ValidationFailure>> ValidateAsync(InitializeNodeRequest request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}