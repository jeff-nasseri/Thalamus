using ErrorHandling;

namespace Infrastructure.Services.Cli.Base;

/// <summary>
///     Base interface for all CLI service operations.
///     Provides the fundamental contract for executing command-line operations.
/// </summary>
public interface ICliService
{
    /// <summary>
    ///     Executes a CLI command with the provided arguments.
    /// </summary>
    /// <param name="args">Command-line arguments to process.</param>
    /// <returns>A response indicating the result of the command execution.</returns>
    Task<Response> ExecuteAsync(string[] args);
}