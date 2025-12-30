using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Application.Common.Extensions;

/// <summary>
/// Provides extension methods for retrieving attribute values from properties.
/// </summary>
public static class AttributeExtensions
{
    /// <summary>
    /// Gets the DisplayName from the DisplayNameAttribute of a property.
    /// </summary>
    /// <typeparam name="T">The type containing the property.</typeparam>
    /// <param name="propertyName">The name of the property.</param>
    /// <returns>The display name from the DisplayNameAttribute.</returns>
    public static string DisplayName<T>(this string propertyName)
    {
        return typeof(T).GetProperty(propertyName)!.GetCustomAttribute<DisplayNameAttribute>()!.DisplayName;
    }

    /// <summary>
    /// Gets the Name from the DisplayAttribute of a property.
    /// </summary>
    /// <typeparam name="T">The type containing the property.</typeparam>
    /// <param name="propertyName">The name of the property.</param>
    /// <returns>The name from the DisplayAttribute.</returns>
    public static string Display<T>(this string propertyName)
    {
        return typeof(T).GetProperty(propertyName)!.GetCustomAttribute<DisplayAttribute>()!.Name!;
    }
}