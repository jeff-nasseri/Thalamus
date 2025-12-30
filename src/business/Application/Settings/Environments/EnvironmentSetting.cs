using Newtonsoft.Json;

namespace Application.Settings.Environments;

/// <summary>
/// Base class for environment-specific settings.
/// Provides JSON serialization for configuration display.
/// </summary>
public abstract class EnvironmentSetting
{
    /// <summary>
    /// Returns a JSON representation of the environment setting.
    /// </summary>
    /// <returns>A JSON string representation of this setting.</returns>
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}