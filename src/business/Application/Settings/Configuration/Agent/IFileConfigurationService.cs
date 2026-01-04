using ErrorHandling;

namespace Application.Settings.Configuration.Agent;

/// <summary>
///     Service interface for registering file-based configurations.
/// </summary>
/// <typeparam name="TFileConfigurationModel">The type of file configuration model to process.</typeparam>
public interface IFileConfigurationService<in TFileConfigurationModel>
    where TFileConfigurationModel : FileConfigurationModel
{
    /// <summary>
    ///     Registers a file configuration model in the system.
    /// </summary>
    /// <param name="fileConfigurationModel">The file configuration model to register.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>A response indicating success or failure of the registration.</returns>
    Task<Response> RegisterConfigurationAsync(TFileConfigurationModel fileConfigurationModel,
        CancellationToken cancellationToken);
}