using System.Reflection;

namespace Application.Common.Util;

/// <summary>
///     Provides utility methods for comparing objects and detecting property changes.
/// </summary>
public class Compare
{
    /// <summary>
    ///     Gets the names of properties that have changed between two objects.
    /// </summary>
    /// <typeparam name="T">The type of the original object.</typeparam>
    /// <typeparam name="Tg">The type of the changed object.</typeparam>
    /// <param name="original">The original object.</param>
    /// <param name="changedObj">The changed object to compare against.</param>
    /// <param name="excludes">Array of property names to exclude from comparison.</param>
    /// <returns>An enumerable of property names that have changed.</returns>
    public static IEnumerable<string> GetChangedPropertyNames<T, Tg>(T original, Tg changedObj,
        string[] excludes = null!) where T : class
        where Tg : class
    {
        List<string> changes = new();
        IEnumerable<PropertyInfo> properties = typeof(T).GetProperties().ToList();

        foreach (var property in properties)
        {
            if (excludes.Any(item => item.ToLower().Contains(property.Name.ToLower()))) continue;

            var originalValue = property.GetValue(original)?.ToString();
            var changedValue = property.GetValue(changedObj)?.ToString();

            if (originalValue != changedValue) changes.Add(property.Name);
        }

        return changes;
    }

    /// <summary>
    ///     Gets the names of properties that have changed between two objects of the same type.
    /// </summary>
    /// <typeparam name="T">The type of both objects.</typeparam>
    /// <param name="original">The original object.</param>
    /// <param name="changedObj">The changed object to compare against.</param>
    /// <param name="excludes">Array of property names to exclude from comparison.</param>
    /// <returns>An enumerable of property names that have changed.</returns>
    public static IEnumerable<string> GetChangedPropertyNames<T>(T original, T changedObj, string[] excludes = null!)
        where T : class
    {
        return GetChangedPropertyNames<T, T>(original, changedObj, excludes);
    }
}