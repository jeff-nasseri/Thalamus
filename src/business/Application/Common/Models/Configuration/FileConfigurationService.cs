using Application.Services.Configuration;
using ErrorHandling;

namespace Application.Common.Models.Configuration;

/// <summary>
///     Service implementation for processing and registering file-based configurations.
/// </summary>
/// <typeparam name="TFileConfigurationModel">The type of file configuration model to process.</typeparam>
public class FileConfigurationService<TFileConfigurationModel> : IFileConfigurationService<TFileConfigurationModel>
    where TFileConfigurationModel : FileConfigurationModel
{
    /// <summary>
    ///     Registers a file configuration model in the system.
    /// </summary>
    /// <param name="fileConfigurationModel">The file configuration model to register.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>A response indicating success or failure of the registration.</returns>
    /// <exception cref="NotImplementedException">This method is not yet implemented.</exception>
    public Task<Response> RegisterConfigurationAsync(TFileConfigurationModel fileConfigurationModel,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}