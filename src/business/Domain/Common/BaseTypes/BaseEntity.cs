using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common.BaseTypes;

/// <summary>
/// Base entity class with a GUID identifier.
/// </summary>
public abstract class BaseEntity : BaseEntity<Guid>
{
}

/// <summary>
/// Base entity class with a strongly-typed identifier.
/// </summary>
/// <typeparam name="T">The type of the entity identifier, constrained to value types.</typeparam>
public abstract class BaseEntity<T> : IBaseEntity<T>
    where T : struct
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual T Id { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the entity was last modified.
    /// </summary>
    public virtual DateTime? ModifiedDateTime { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the entity was created.
    /// </summary>
    public virtual DateTime CreatedDateTime { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the date and time when the entity was soft deleted.
    /// </summary>
    public virtual DateTime? DeletedDateTime { get; set; }

    /// <summary>
    /// Marks the entity as soft deleted by setting the DeletedDateTime to the current UTC time.
    /// </summary>
    public virtual void SoftDelete()
    {
        DeletedDateTime = DateTime.UtcNow;
    }
}