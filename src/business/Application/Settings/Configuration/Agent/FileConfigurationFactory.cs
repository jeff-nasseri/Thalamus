using ErrorHandling;

namespace Application.Settings.Configuration.Agent;

/// <summary>
///     Factory for registering file-based configuration models.
/// </summary>
/// <typeparam name="TConfigurationModel">The type of file configuration model to register.</typeparam>
internal class FileConfigurationFactory<TConfigurationModel> : ConfigurationFactory<TConfigurationModel>
    where TConfigurationModel : FileConfigurationModel
{
    /// <summary>
    ///     Registers multiple file-based configuration models asynchronously.
    /// </summary>
    /// <param name="configurationModels">The collection of file configuration models to register.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>A response indicating success or failure of the registration.</returns>
    /// <exception cref="NotImplementedException">This method is not yet implemented.</exception>
    public override Task<Response> RegisterConfigurationAsync(IEnumerable<TConfigurationModel> configurationModels,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}