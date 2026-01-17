using Infrastructure.Services.Cli.Thalamus.Configuration;
using Infrastructure.Services.Cli.Thalamus.Memory;
using Infrastructure.Services.Cli.Thalamus.Prompt;

namespace Infrastructure.Services.Cli.Thalamus;

/// <summary>
///     Main interface for the Thalamus CLI service.
///     Aggregates all CLI functionality including configuration management, prompt execution, and memory access.
/// </summary>
public interface IThalamusCliService : IThalamusCliConfigurationAccess, IThalamusCliPromptAccess,
    IThalamusCliMemoryAccess
{
}