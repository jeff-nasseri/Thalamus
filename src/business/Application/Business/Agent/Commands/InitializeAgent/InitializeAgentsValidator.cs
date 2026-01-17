using Application.Common.Validators;
using FluentValidation.Results;

namespace Application.Business.Agent.Commands.InitializeAgent;

public class InitializeAgentsValidator : IRequestValidator<InitializeAgentsRequest>
{
    public Task<List<ValidationFailure>> ValidateAsync(InitializeAgentsRequest request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}