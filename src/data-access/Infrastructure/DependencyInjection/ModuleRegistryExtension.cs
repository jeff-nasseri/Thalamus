using System.Reflection;
using Application.Common.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection;

/// <summary>
///     Extension methods for automatic module registration in dependency injection container.
/// </summary>
public static class ModuleRegistryExtension
{
    /// <summary>
    ///     Automatically discovers and registers all IModuleConfiguration implementations from the specified assemblies.
    /// </summary>
    /// <param name="service">The service collection to register modules into.</param>
    /// <param name="assemblies">The assemblies to scan for module configurations.</param>
    /// <returns>The service collection for method chaining.</returns>
    /// <remarks>
    ///     This method uses reflection to find all types that implement IModuleConfiguration,
    ///     creates instances of those types, and calls their RegisterConfiguration method
    ///     to register their services with the dependency injection container.
    /// </remarks>
    public static IServiceCollection ModuleRegistry(this IServiceCollection service, params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            List<Type> types = assembly.GetTypes()
                .Where(t => typeof(IModuleConfiguration).IsAssignableFrom(t) &&
                            !string.Equals(t.Name, nameof(IModuleConfiguration),
                                StringComparison.CurrentCultureIgnoreCase))
                .ToList();

            foreach (var obj in types.Select(type =>
                         (IModuleConfiguration)(Activator.CreateInstance(type) ?? throw new Exception())))
                obj.RegisterConfiguration(service);
        }

        return service;
    }
}