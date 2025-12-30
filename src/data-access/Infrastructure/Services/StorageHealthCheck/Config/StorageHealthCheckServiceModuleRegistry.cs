using Application.Common.Configuration;
using Application.Services.StorageHealthCheck;
using Infrastructure.Services.StorageHealthCheck.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services.StorageHealthCheck.Config;

/// <summary>
/// Configuration module for storage health check service dependency injection registration.
/// </summary>
public class StorageHealthCheckServiceModuleRegistry : IModuleConfiguration
{
    /// <summary>
    /// Registers storage health check service dependencies into the service collection.
    /// </summary>
    /// <param name="services">The service collection to register services into.</param>
    /// <returns>The service collection for method chaining.</returns>
    public IServiceCollection RegisterConfiguration(IServiceCollection services)
    {
        services.AddScoped<IStorageHealthCheckService, StorageHealthCheckService>();

        return services;
    }
}