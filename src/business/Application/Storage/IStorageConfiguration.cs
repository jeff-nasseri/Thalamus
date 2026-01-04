using Application.Common.BaseTypes;
using Application.Common.Enums;

namespace Application.Storage;

/// <summary>
///     Interface for memory storage configuration.
///     Defines the contract for configuring memory storage type and connection settings.
/// </summary>
public interface IStorageConfiguration : IConfiguration
{
    /// <summary>
    ///     Gets the type of memory storage system to use.
    /// </summary>
    MemoryStorageType StorageType { get; }
}