namespace ErrorHandling.Interfaces;

/// <summary>
/// Generic interface for exceptions that provide a typed error code.
/// </summary>
/// <typeparam name="TEnum">The enum type representing the error code.</typeparam>
public interface ICodedException<out TEnum> : ICodedException
    where TEnum : Enum
{
    /// <summary>
    /// Gets the error code associated with this exception.
    /// </summary>
    /// <returns>The error code enum value.</returns>
    public TEnum GetErrorCode();
}

/// <summary>
/// Base marker interface for exceptions that provide error codes.
/// </summary>
public interface ICodedException
{
}