using ErrorHandling;

namespace Infrastructure.Services.Cli.Thalamus.Memory;

/// <summary>
/// Implementation of memory access services for the Thalamus CLI.
/// Provides functionality to retrieve and export application memory data.
/// </summary>
public class ThalamusCliMemoryAccess : IThalamusCliMemoryAccess
{
    /// <inheritdoc />
    public Task<Response> ExecuteAsync(string[] args)
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