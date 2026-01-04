using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Domain.Common.BaseTypes;
using Domain.Exceptions;

namespace Domain.ValueObjects;

/// <summary>
///     Represents an email address value object with validation.
/// </summary>
public class Email : ValueObject, IValueObjectParser<Email, string>
{
    /// <summary>
    ///     Gets or sets the full email address.
    /// </summary>
    [MaxLength(50)]
    [DisplayName("MailAddress")]
    [Display(Name = "MailAddress")]
    public virtual string MailAddress { get; set; } = null!;

    /// <summary>
    ///     Gets or sets the host portion of the email address (domain after '@').
    /// </summary>
    [MaxLength(50)]
    [DisplayName("MailHost")]
    [Display(Name = "MailHost")]
    public virtual string MailHost { get; set; } = null!;

    /// <summary>
    ///     Attempts to parse a string into an Email value object.
    /// </summary>
    /// <param name="emailAddress">The email address string to parse.</param>
    /// <param name="email">The parsed Email object if successful; otherwise, null.</param>
    /// <returns>True if parsing was successful; otherwise, false.</returns>
    public static bool TryParse(string emailAddress, out Email? email)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(emailAddress))
            {
                email = null;
                return false;
            }

            var index = emailAddress.IndexOf('@');

            var isValid =
                index > 0 &&
                index != emailAddress.Length - 1 &&
                index == emailAddress.LastIndexOf('@');

            if (isValid)
            {
                email = new Email
                {
                    MailAddress = emailAddress,
                    MailHost = emailAddress.Split("@")[1]
                };
                return true;
            }

            email = null;
            return false;
        }
        catch (Exception)
        {
            email = null;
            return false;
        }
    }

    /// <summary>
    ///     Parses a string into an Email value object.
    /// </summary>
    /// <param name="emailAddress">The email address string to parse.</param>
    /// <returns>The parsed Email object.</returns>
    /// <exception cref="InvalidEmailException">Thrown when the email address is invalid.</exception>
    public static Email Parse(string emailAddress)
    {
        var result = TryParse(emailAddress, out var email);

        if (!result || email == null) throw new InvalidEmailException(emailAddress);

        return email;
    }

    /// <summary>
    ///     Gets the components that define the equality of the Email value object.
    /// </summary>
    /// <returns>An enumerable of objects that represent the equality components.</returns>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return MailAddress;
        yield return MailHost;
    }

    /// <summary>
    ///     Returns the string representation of the email address.
    /// </summary>
    /// <returns>The full email address.</returns>
    public override string ToString()
    {
        return MailAddress;
    }
}