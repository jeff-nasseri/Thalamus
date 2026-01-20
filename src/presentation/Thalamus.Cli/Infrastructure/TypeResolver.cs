using Spectre.Console.Cli;

namespace Thalamus.Cli.Infrastructure;

/// <summary>
///     Type resolver for integrating Microsoft.Extensions.DependencyInjection with Spectre.Console.
/// </summary>
public sealed class TypeResolver : ITypeResolver
{
    private readonly IServiceProvider _provider;

    public TypeResolver(IServiceProvider provider)
    {
        _provider = provider;
    }

    public object? Resolve(Type? type)
    {
        return type == null ? null : _provider.GetService(type);
    }

    public void Dispose()
    {
        if (_provider is IDisposable disposable) disposable.Dispose();
    }
}