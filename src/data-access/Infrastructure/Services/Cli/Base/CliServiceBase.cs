// See https://aka.ms/new-console-template for more information

using ErrorHandling;

namespace Infrastructure.Services.Cli.Base;

/// <summary>
///     Abstract base class for CLI service implementations.
///     Provides common functionality and structure for CLI service operations.
/// </summary>
internal abstract class CliServiceBase : ICliService
{
    /// <inheritdoc />
    public Task<Response> ExecuteAsync(string[] args)
    {
        throw new NotImplementedException();
    }
}