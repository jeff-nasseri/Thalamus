using ErrorHandling;
using Infrastructure.Services.Cli.Thalamus.Configuration;
using Infrastructure.Services.Cli.Thalamus.Memory;
using Infrastructure.Services.Cli.Thalamus.Prompt;

namespace Infrastructure.Services.Cli.Thalamus;

/// <summary>
/// Main implementation of the Thalamus CLI service.
/// Orchestrates all CLI operations by delegating to specialized service implementations.
/// </summary>
public class ThalamusCliService : IThalamusCliService
{
    private readonly IThalamusCliConfigurationAccess _configurationAccess;
    private readonly IThalamusCliPromptAccess _promptAccess;
    private readonly IThalamusCliMemoryAccess _memoryAccess;

    /// <summary>
    /// Initializes a new instance of the <see cref="ThalamusCliService"/> class.
    /// </summary>
    /// <param name="configurationAccess">The configuration access service.</param>
    /// <param name="promptAccess">The prompt access service.</param>
    /// <param name="memoryAccess">The memory access service.</param>
    /// <exception cref="ArgumentNullException">Thrown when any required dependency is null.</exception>
    public ThalamusCliService(
        IThalamusCliConfigurationAccess configurationAccess,
        IThalamusCliPromptAccess promptAccess,
        IThalamusCliMemoryAccess memoryAccess)
    {
        _configurationAccess = configurationAccess ?? throw new ArgumentNullException(nameof(configurationAccess));
        _promptAccess = promptAccess ?? throw new ArgumentNullException(nameof(promptAccess));
        _memoryAccess = memoryAccess ?? throw new ArgumentNullException(nameof(memoryAccess));
    }

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

    /// <inheritdoc />
    public Task<Response> GetMemoryAsync(string[] args)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<Response> ExportMemoryAsync(string[] args)
    {
        throw new NotImplementedException();
    }
}