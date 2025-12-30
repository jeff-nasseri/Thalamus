using ErrorHandling.Enums;

namespace ErrorHandling.Attributes;

/// <summary>
/// Attribute to associate a handler code with an enum or class.
/// Used to prefix error codes with handler-specific identifiers.
/// </summary>
[AttributeUsage(AttributeTargets.Enum | AttributeTargets.Class)]
public class HandlerCodeAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HandlerCodeAttribute"/> class.
    /// </summary>
    /// <param name="handlerCode">The handler code to associate with the target.</param>
    public HandlerCodeAttribute(HandlerCode handlerCode)
    {
        HandlerCode = handlerCode;
    }

    /// <summary>
    /// Gets the handler code.
    /// </summary>
    public HandlerCode HandlerCode { get; }
}