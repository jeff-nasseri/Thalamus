using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Application.Common.Util;

/// <summary>
/// Provides utility methods for working with enum types.
/// </summary>
public class EnumUtils
{
    /// <summary>
    /// Gets an enum value by matching its DisplayAttribute name.
    /// </summary>
    /// <typeparam name="T">The enum type.</typeparam>
    /// <param name="name">The display name to search for (case-insensitive).</param>
    /// <returns>The enum value matching the display name, or default if not found.</returns>
    public static T GetEnumByString<T>(string name)
        where T : Enum
    {
        FieldInfo? field = typeof(T).GetFields().SingleOrDefault(p =>
            p.GetCustomAttribute<DisplayAttribute>() != null &&
            p.GetCustomAttribute<DisplayAttribute>()!.Name!.Equals(name, StringComparison.OrdinalIgnoreCase));

        if (field is null)
        {
            return default!;
        }

        string? value = field.GetValue(null)!.ToString();

        if (string.IsNullOrEmpty(value))
        {
            return default!;
        }

        return (T)Enum.Parse(typeof(T), value, true);
    }
}