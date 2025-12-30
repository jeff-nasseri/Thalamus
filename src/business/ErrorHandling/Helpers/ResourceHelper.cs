using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace ErrorHandling.Helpers;

/// <summary>
/// Helper class for retrieving error messages from resource files.
/// </summary>
public static class ResourceHelper
{
    /// <summary>
    /// The default culture used for error messages.
    /// </summary>
    public const string DEFAULT_CULTURE = "en-US";

    /// <summary>
    /// Gets all error messages from resource files in the given assembly.
    /// </summary>
    /// <param name="assembly">The assembly to search for resource files.</param>
    /// <param name="cultureInfo">The culture for which to retrieve messages. Defaults to en-US if not specified.</param>
    /// <returns>A dictionary with error code keys (format: dd_ddd_ddd) and their corresponding message values.</returns>
    public static Dictionary<string, string> GetAllErrorMessages(Assembly assembly, CultureInfo? cultureInfo = null)
    {
        cultureInfo ??= new CultureInfo(DEFAULT_CULTURE);

        string[] xmlResourceNames = assembly.GetManifestResourceNames()
            .Select(s => s.Replace(".resources", string.Empty))
            .ToArray();

        Dictionary<string, string> errorMessages = new();

        foreach (string? xmlResourceName in xmlResourceNames)
        {
            ResourceManager resourceManager = new(xmlResourceName, assembly);
            ResourceSet? resourceSet = resourceManager.GetResourceSet(cultureInfo, true, true);

            if (resourceSet is null)
            {
                continue;
            }

            foreach (DictionaryEntry entry in resourceSet)
            {
                string? key = entry.Key.ToString();

                // Only include keys that have error code format (dd_ddd_ddd)
                if (key is not null && key.Length == 10 && key[2] == '_' && key[6] == '_')
                {
                    errorMessages.Add(key, entry.Value?.ToString() ?? string.Empty);
                }
            }
        }

        return errorMessages.OrderBy(kvp => kvp.Key).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    }

    /// <summary>
    /// Retrieves an error message from a resource file based on the provided key.
    /// </summary>
    /// <typeparam name="TResource">The type of the resource file to access.</typeparam>
    /// <param name="key">The key of the error message in the resource file.</param>
    /// <param name="cultureInfo">The culture for which to retrieve the message. Defaults to en-US if not specified.</param>
    /// <returns>The error message as a string, or an empty string if the message is not found.</returns>
    public static string GetDefaultErrorMessage<TResource>(string key, CultureInfo? cultureInfo = null)
    {
        cultureInfo ??= new CultureInfo(DEFAULT_CULTURE);
        ResourceManager resourceManager = new(typeof(TResource));
        return resourceManager.GetString(key, cultureInfo) ?? string.Empty;
    }
}