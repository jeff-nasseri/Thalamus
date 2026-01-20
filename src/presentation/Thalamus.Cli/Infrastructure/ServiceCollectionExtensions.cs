using Infrastructure.Services.Cli.Thalamus;
using Infrastructure.Services.Cli.Thalamus.Configuration;
using Infrastructure.Services.Cli.Thalamus.Memory;
using Infrastructure.Services.Cli.Thalamus.Prompt;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Thalamus.Cli.Infrastructure;

/// <summary>
///     Extension methods for configuring CLI services in the DI container.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     Adds Thalamus CLI services to the service collection.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    /// <param name="baseConfigurationPath">The base path for configuration files.</param>
    /// <returns>The configured service collection.</returns>
    public static IServiceCollection AddThalamusCliServices(
        this IServiceCollection services,
        string? baseConfigurationPath = null)
    {
        // Register the base configuration path
        var configPath = baseConfigurationPath ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory);

        // Register CLI services
        services.AddSingleton<IThalamusCliConfigurationAccess>(sp =>
        {
            var mediator = sp.GetRequiredService<IMediator>();
            return new ThalamusCliConfigurationAccess(mediator, configPath);
        });

        services.AddSingleton<IThalamusCliMemoryAccess, ThalamusCliMemoryAccess>();
        services.AddSingleton<IThalamusCliPromptAccess, ThalamusCliPromptAccess>();
        services.AddSingleton<IThalamusCliService, ThalamusCliService>();

        return services;
    }
}