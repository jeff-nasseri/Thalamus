using Microsoft.Extensions.DependencyInjection;

namespace Application.Common.Configuration;

/// <summary>
///     Defines a contract for module configuration registration.
///     Implement this interface to register module-specific services and configurations.
/// </summary>
public interface IModuleConfiguration
{
    /// <summary>
    ///     Registers module-specific configurations and services to the service collection.
    /// </summary>
    /// <param name="services">The service collection to register services into.</param>
    /// <returns>The configured service collection.</returns>
    IServiceCollection RegisterConfiguration(IServiceCollection services);
}