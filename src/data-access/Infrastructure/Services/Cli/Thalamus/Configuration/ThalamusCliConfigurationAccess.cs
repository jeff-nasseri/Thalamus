using ErrorHandling;

namespace Infrastructure.Services.Cli.Thalamus.Configuration;

/// <summary>
/// Implementation of configuration access services for the Thalamus CLI.
/// Handles configuration initialization and dashboard setup operations.
/// </summary>
public class ThalamusCliConfigurationAccess : IThalamusCliConfigurationAccess
{
    /// <inheritdoc />
    public Task<Response> ExecuteAsync(string[] args)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<Response> IdempotentConfigurationInitializationAsync(string[] args)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<Response> SetupWebDashboardAsync(string[] args)
    {
        throw new NotImplementedException();
    }
}