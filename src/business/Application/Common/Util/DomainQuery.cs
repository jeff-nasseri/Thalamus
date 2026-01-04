using BaseEntity = Domain.Common.BaseTypes.BaseEntity;

namespace Application.Common.Util;

/// <summary>
///     Provides utility methods for building domain queries and filters.
/// </summary>
public static class DomainQuery
{
    /// <summary>
    ///     Creates a DateTime range filter function for domain entities.
    ///     Returns a predicate that filters entities by their CreatedDateTime within the specified range.
    /// </summary>
    /// <typeparam name="T">The entity type inheriting from BaseEntity.</typeparam>
    /// <param name="from">The start date of the range (inclusive).</param>
    /// <param name="to">The end date of the range (inclusive).</param>
    /// <returns>
    ///     A predicate function that returns true for entities within the date range, or always true if dates are
    ///     invalid.
    /// </returns>
    public static Func<T, bool> ValidateDefaultDateTimeFilter<T>(DateTime? from, DateTime? to)
        where T : BaseEntity
    {
        if (from is null || to is null || from.Equals(default(DateTime)) || to.Equals(default(DateTime)))
            return _ => true;

        return Func;

        bool Func(T t)
        {
            var result = t.CreatedDateTime >= from && t.CreatedDateTime <= to;
            return result;
        }
    }
}