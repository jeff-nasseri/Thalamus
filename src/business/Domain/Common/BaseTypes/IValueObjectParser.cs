namespace Domain.Common.BaseTypes;

/// <summary>
/// Defines a contract for parsing input values into value objects.
/// </summary>
/// <typeparam name="TValueObject">The value object type to parse into.</typeparam>
/// <typeparam name="T">The input type to parse from.</typeparam>
public interface IValueObjectParser<TValueObject, in T> where TValueObject : ValueObject
{
    /// <summary>
    /// Attempts to parse the input into a specific value object type.
    /// </summary>
    /// <param name="input">The input value to parse.</param>
    /// <param name="valueObject">The parsed value object if successful; otherwise, null.</param>
    /// <returns>True if parsing was successful; otherwise, false.</returns>
    static abstract bool TryParse(T input, out TValueObject? valueObject);

    /// <summary>
    /// Parses the input into a specific value object type.
    /// </summary>
    /// <param name="input">The input value to parse.</param>
    /// <returns>The parsed value object.</returns>
    /// <exception cref="System.Exception">Thrown when the input format is invalid.</exception>
    static abstract TValueObject Parse(T input);
}