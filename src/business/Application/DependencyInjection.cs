using System.Reflection;
using Application.Common.PipelineBehaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

/// <summary>
///     Provides dependency injection configuration for the Application layer.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    ///     Registers Application layer services including MediatR and FluentValidation.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    /// <returns>The configured service collection.</returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.AddOpenBehavior(typeof(CommonErrorsPipelineBehavior<,>));
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
        });

        services.AddValidatorsFromAssemblies(new List<Assembly> { Assembly.GetExecutingAssembly() });

        return services;
    }
}