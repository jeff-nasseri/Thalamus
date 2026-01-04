using ErrorHandling;

namespace Application.Storage;

/// <summary>
///     Base interface for memory storage access operations.
///     Provides fundamental connectivity testing for memory storage systems.
/// </summary>
public interface IMemoryAccess
{
    /// <summary>
    ///     Tests connectivity to the memory storage system.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>A response indicating success or failure of the connectivity test.</returns>
    Task<Response> TryPingMemoryAsync(CancellationToken cancellationToken);
}