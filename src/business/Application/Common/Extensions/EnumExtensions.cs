using System.ComponentModel.DataAnnotations;

namespace Application.Common.Extensions;

/// <summary>
///     Provides extension methods for working with enum types and their attributes.
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    ///     Gets the display string from the DisplayAttribute of an enum value.
    /// </summary>
    /// <typeparam name="TEnum">The enum type.</typeparam>
    /// <param name="enum">The enum value.</param>
    /// <returns>The display name from the DisplayAttribute.</returns>
    public static string DisplayString<TEnum>(this TEnum @enum) where TEnum : Enum
    {
        var result = @enum.GetAttributeOfType<DisplayAttribute>()?.Name;
        return result!;
    }

    /// <summary>
    ///     Gets an attribute on an enum field value.
    /// </summary>
    /// <typeparam name="T">The type of the attribute to retrieve.</typeparam>
    /// <param name="enumVal">The enum value.</param>
    /// <returns>The attribute of type T that exists on the enum value, or null if not found.</returns>
    /// <example>
    ///     <code>
    /// string desc = myEnumVariable.GetAttributeOfType&lt;DescriptionAttribute&gt;().Description;
    /// </code>
    /// </example>
    public static T? GetAttributeOfType<T>(this Enum enumVal) where T : Attribute
    {
        var type = enumVal.GetType();
        var memInfo = type.GetMember(enumVal.ToString());
        var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
        return attributes.Length > 0 ? (T)attributes[0] : null;
    }
}