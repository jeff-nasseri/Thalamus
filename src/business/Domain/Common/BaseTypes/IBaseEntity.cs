namespace Domain.Common.BaseTypes;

/// <summary>
/// Base interface for all domain entities with a strongly-typed identifier.
/// </summary>
/// <typeparam name="TId">The type of the entity identifier, constrained to value types.</typeparam>
public interface IBaseEntity<TId> where TId : struct
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// </summary>
    TId Id { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the entity was last modified.
    /// </summary>
    DateTime? ModifiedDateTime { get; set; }

    /// <summary>
    /// Gets the date and time when the entity was created.
    /// </summary>
    DateTime CreatedDateTime { get; }

    /// <summary>
    /// Gets the date and time when the entity was soft deleted.
    /// </summary>
    DateTime? DeletedDateTime { get; }
}