using Application.Common.Enums;

namespace Application.Common.Models.Configuration;

/// <summary>
///     Base class for file-based configuration models.
///     Represents configurations that are loaded from external files.
/// </summary>
public abstract class FileConfigurationModel : ConfigurationModel
{
    /// <summary>
    ///     Gets the type of configuration model.
    /// </summary>
    public abstract ConfigurationModelType ConfigurationModelType { get; }
}