using Application.Common.Configuration;
using Application.Services.Ip;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services.Ip.Config;

/// <summary>
///     Configuration module for IP service dependency injection registration.
/// </summary>
public class IpServiceConfiguration : IModuleConfiguration
{
    /// <summary>
    ///     Registers IP service dependencies into the service collection.
    /// </summary>
    /// <param name="services">The service collection to register services into.</param>
    /// <returns>The service collection for method chaining.</returns>
    public IServiceCollection RegisterConfiguration(IServiceCollection services)
    {
        services.AddScoped<IIpService, IpService>();
        return services;
    }
}