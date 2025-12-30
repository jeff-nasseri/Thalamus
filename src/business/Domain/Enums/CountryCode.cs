using System.ComponentModel.DataAnnotations;

namespace Domain.Enums;

/// <summary>
/// Represents the platform-supported country codes.
/// </summary>
public enum CountryCode
{
    /// <summary>
    /// United States country code.
    /// </summary>
    [Display(Name = "US")]
    Us
}