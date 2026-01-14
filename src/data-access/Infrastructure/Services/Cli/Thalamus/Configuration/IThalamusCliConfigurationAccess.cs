using ErrorHandling;
using Infrastructure.Services.Cli.Base;

namespace Infrastructure.Services.Cli.Thalamus.Configuration;

/// <summary>
/// Provides configuration management access for the Thalamus CLI.
/// Handles initialization of configuration data and dashboard setup.
/// </summary>
public interface IThalamusCliConfigurationAccess : ICliService
{
    /// <summary>
    /// Performs idempotent initialization of configuration in the data storage.
    /// Loads and applies configuration from MCP (Model Context Protocol), agent, and nodes configuration files.
    /// This operation can be safely called multiple times without side effects.
    /// </summary>
    /// <param name="args">Command arguments specifying which configurations to initialize (mcp, agent, nodes).</param>
    /// <returns>A response indicating the result of the configuration initialization.</returns>
    Task<Response> IdempotentConfigurationInitializationAsync(string[] args);

    /// <summary>
    /// Sets up the web dashboard UI based on the current configuration.
    /// </summary>
    /// <param name="args">Command arguments including the optional URL for the dashboard. Defaults to 'localhost:5055' if not specified.</param>
    /// <returns>A response indicating the result of the dashboard setup.</returns>
    Task<Response> SetupWebDashboardAsync(string[] args);
}