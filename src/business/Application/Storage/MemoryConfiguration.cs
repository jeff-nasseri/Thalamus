using Application.Common.Enums;

namespace Application.Storage;

/// <summary>
///     Implementation of memory storage configuration.
///     Provides configuration settings for the agent memory subsystem.
/// </summary>
public class MemoryConfiguration : IStorageConfiguration
{
    /// <summary>
    ///     Gets the memory storage type.
    ///     Currently configured to use NoSQL storage for optimal memory performance.
    /// </summary>
    public MemoryStorageType StorageType => MemoryStorageType.NO_SQL;
}