namespace Application.Common.Attributes;

/// <summary>
/// Attribute to specify the execution order of handler steps.
/// Applied to methods to define their sequence in a multi-step handler process.
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class HandlerStepAttribute : Attribute
{
    /// <summary>
    /// Gets or sets the execution order of the handler step.
    /// Lower values execute first.
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="HandlerStepAttribute"/> class.
    /// </summary>
    /// <param name="order">The execution order of the handler step.</param>
    public HandlerStepAttribute(int order)
    {
        Order = order;
    }
}