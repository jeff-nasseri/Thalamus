using Application.Common.Validators;
using FluentValidation.Results;

namespace Application.Business.Mcp.Commands.InitializeMcp;

public class InitializeMcpValidator : IRequestValidator<InitializeMcpRequest>
{
    public Task<List<ValidationFailure>> ValidateAsync(InitializeMcpRequest request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}