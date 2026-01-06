using Application.Common.Models.Configuration;
using ErrorHandling;

namespace Application.Common.Factories.Agent;

/// <summary>
///     Abstract factory for registering configuration models in the system.
/// </summary>
/// <typeparam name="TConfigurationModel">The type of configuration model to register.</typeparam>
internal abstract class ConfigurationFactory<TConfigurationModel> where TConfigurationModel : ConfigurationModel
{
    /// <summary>
    ///     Registers multiple configuration models asynchronously.
    /// </summary>
    /// <param name="configurationModels">The collection of configuration models to register.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>A response indicating success or failure of the registration.</returns>
    public abstract Task<Response> RegisterConfigurationAsync(
        IEnumerable<TConfigurationModel> configurationModels, CancellationToken cancellationToken);
}