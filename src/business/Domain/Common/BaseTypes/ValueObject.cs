namespace Domain.Common.BaseTypes;

/// <summary>
///     Base class for value objects in the domain.
///     Value objects are immutable objects that are defined by their attributes rather than a unique identity.
/// </summary>
public abstract class ValueObject
{
    /// <summary>
    ///     Determines whether the specified object is equal to the current value object.
    /// </summary>
    /// <param name="obj">The object to compare with the current value object.</param>
    /// <returns>True if the specified object is equal to the current value object; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
        if (obj == null) return false;

        if (GetType() != obj.GetType()) return false;

        var valueObject = (ValueObject)obj;

        return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
    }

    /// <summary>
    ///     Serves as the default hash function.
    /// </summary>
    /// <returns>A hash code for the current value object.</returns>
    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Aggregate(1, (current, obj) =>
            {
                unchecked
                {
                    return current * 23 + obj.GetHashCode();
                }
            });
    }

    /// <summary>
    ///     Gets the components that define the equality of the value object.
    /// </summary>
    /// <returns>An enumerable of objects that represent the equality components.</returns>
    protected abstract IEnumerable<object> GetEqualityComponents();
}