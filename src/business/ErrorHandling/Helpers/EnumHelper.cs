namespace ErrorHandling.Helpers;

/// <summary>
/// Generic helper class for retrieving custom attributes from enum values.
/// </summary>
/// <typeparam name="TAttribute">The type of attribute to retrieve.</typeparam>
/// <typeparam name="TEnum">The enum type.</typeparam>
public static class EnumHelper<TAttribute, TEnum>
    where TEnum : Enum
    where TAttribute : Attribute
{
    /// <summary>
    /// Gets all custom attributes of the specified type from an enum value.
    /// </summary>
    /// <param name="enum">The enum value.</param>
    /// <returns>A collection of custom attributes.</returns>
    public static IEnumerable<TAttribute> GetCustomAttributes(TEnum @enum)
    {
        Type enumType = @enum.GetType();
        string? name = Enum.GetName(enumType, @enum);
        IEnumerable<TAttribute>? result = enumType.GetField(name!)!.GetCustomAttributes(true)
            .OfType<TAttribute>().ToList();

        return result;
    }

    /// <summary>
    /// Gets a single custom attribute of the specified type from an enum value.
    /// </summary>
    /// <param name="enum">The enum value.</param>
    /// <returns>The custom attribute, or null if not found.</returns>
    public static TAttribute GetCustomAttribute(TEnum @enum)
    {
        Type enumType = @enum.GetType();
        string? name = Enum.GetName(enumType, @enum);
        TAttribute? result = enumType.GetField(name!)!.GetCustomAttributes(false)
            .OfType<TAttribute>().SingleOrDefault();

        return result!;
    }
}

/// <summary>
/// Helper class providing extension methods for enum operations.
/// </summary>
public static class EnumHelper
{
    /// <summary>
    /// Gets the name of the enum value as a string.
    /// </summary>
    /// <param name="enum">The enum value.</param>
    /// <returns>The name of the enum value.</returns>
    public static string GetName(this Enum @enum)
    {
        Type enumType = @enum.GetType();
        string? name = Enum.GetName(enumType, @enum);
        return name!;
    }
}