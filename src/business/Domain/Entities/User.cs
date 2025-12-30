using System.ComponentModel.DataAnnotations;
using Domain.ValueObjects;
using BaseEntity = Domain.Common.BaseTypes.BaseEntity;

namespace Domain.Entities;

/// <summary>
/// Represents a user entity in the system.
/// </summary>
public class User : BaseEntity
{
    /// <summary>
    /// Gets or sets the username of the user.
    /// Maximum length is 150 characters.
    /// </summary>
    [MaxLength(150)]
    public string? Username { get; set; }

    /// <summary>
    /// Gets or sets the email address of the user.
    /// </summary>
    public Email? EmailAddress { get; set; }
}