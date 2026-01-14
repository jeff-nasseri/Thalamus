using ErrorHandling;

namespace Infrastructure.Services.Cli.Thalamus.Prompt;

/// <summary>
/// Implementation of prompt access services for the Thalamus CLI.
/// Handles communication with the agent through prompt execution.
/// </summary>
public class ThalamusCliPromptAccess : IThalamusCliPromptAccess
{
    /// <inheritdoc />
    public Task<Response> ExecuteAsync(string[] args)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<Response> ExecutePromptAsync(string[] args)
    {
        throw new NotImplementedException();
    }
}