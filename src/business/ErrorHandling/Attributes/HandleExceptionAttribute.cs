namespace ErrorHandling.Attributes;

/// <summary>
///     Attribute to specify custom exception handling for a method.
///     Can be applied multiple times to handle different exception types with different handlers.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
public class HandleExceptionAttribute : Attribute
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="HandleExceptionAttribute" /> class.
    /// </summary>
    /// <param name="exceptionType">The type of exception to handle.</param>
    /// <param name="exceptionHandlerType">The type containing the handler method.</param>
    /// <param name="exceptionHandlerMethodName">The name of the handler method.</param>
    public HandleExceptionAttribute(Type exceptionType, Type exceptionHandlerType, string exceptionHandlerMethodName)
    {
        ExceptionHandlerMethodName = exceptionHandlerMethodName;
        ExceptionType = exceptionType;
        ExceptionHandlerType = exceptionHandlerType;
    }

    /// <summary>
    ///     Gets or sets the name of the method that will handle the exception.
    /// </summary>
    public string ExceptionHandlerMethodName { get; set; }

    /// <summary>
    ///     Gets or sets the type that contains the exception handler method.
    /// </summary>
    public Type ExceptionHandlerType { get; set; }

    /// <summary>
    ///     Gets or sets the type of exception to be handled.
    /// </summary>
    public Type ExceptionType { get; set; }
}