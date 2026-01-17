using System.ComponentModel;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Common;

/// <summary>
///     Utility class for managing environment variables and configuration settings.
/// </summary>
public class EnvUtils
{
    /// <summary>
    ///     Sets up the environment by loading variables from a .env file located in the current directory.
    /// </summary>
    /// <remarks>
    ///     Follow the .env.template and add .env file in your project root and in bin folder of your project.
    /// </remarks>
    public static void SetupEnvFile()
    {
        var envFilePath = Path.Combine(Directory.GetCurrentDirectory(), ".env");
        DotEnv.Load(envFilePath);
    }

    /// <summary>
    ///     Creates and populates an instance of type T by reading environment variables based on
    ///     ConfigurationKeyNameAttribute.
    /// </summary>
    /// <typeparam name="T">The type to instantiate and populate with environment variable values.</typeparam>
    /// <returns>An instance of type T with properties populated from environment variables.</returns>
    /// <remarks>
    ///     This method uses reflection to find properties decorated with ConfigurationKeyNameAttribute,
    ///     reads the corresponding environment variable values, and sets them on the created instance.
    ///     If a property cannot be set (due to missing environment variable or conversion issues), it is silently ignored.
    /// </remarks>
    public static T GetEnvironment<T>()
    {
        IEnumerable<PropertyInfo> properties = typeof(T).GetProperties()
            .Where(p => p.GetCustomAttribute<ConfigurationKeyNameAttribute>() != null);

        var obj = (T)Activator.CreateInstance(typeof(T))!;

        foreach (var property in properties)
            try
            {
                var attrValue = property.GetCustomAttribute<ConfigurationKeyNameAttribute>()!.Name;
                var converter = TypeDescriptor.GetConverter(property.PropertyType);
                var envValue = Environment.GetEnvironmentVariable(attrValue)!;
                property.SetValue(obj, converter.ConvertFromString(envValue));
            }
            catch (Exception)
            {
                // Ignored: Properties with missing or invalid environment variables are skipped
            }

        return obj;
    }
}