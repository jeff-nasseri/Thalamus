using System.ComponentModel.DataAnnotations;

namespace Application.Common.Extensions;

/// <summary>
///     Provides extension methods for array and enumerable operations.
/// </summary>
public static class ArrayExtensions
{
    /// <summary>
    ///     Converts an enumerable of enum values to an array of their string representations.
    /// </summary>
    /// <typeparam name="TEnum">The enum type.</typeparam>
    /// <param name="enums">The enumerable of enum values.</param>
    /// <returns>An array of string representations of the enum values.</returns>
    public static string[] ToStringArray<TEnum>(this IEnumerable<TEnum> enums) where TEnum : Enum
    {
        var result = enums.Select(e => e.ToString()).ToArray();
        return result;
    }

    /// <summary>
    ///     Converts an enumerable of enum values to an array of their display names from DisplayAttribute.
    /// </summary>
    /// <typeparam name="TEnum">The enum type.</typeparam>
    /// <param name="enums">The enumerable of enum values.</param>
    /// <returns>An array of display names from the DisplayAttribute.</returns>
    public static string[] DisplayStringArray<TEnum>(this IEnumerable<TEnum> enums) where TEnum : Enum
    {
        string[] result = enums.Select(e => e.GetAttributeOfType<DisplayAttribute>()!.Name).ToArray()!;
        return result;
    }
}