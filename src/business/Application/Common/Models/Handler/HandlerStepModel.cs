namespace Application.Common.Models.Handler;

/// <summary>
///     Represents a handler step with its name and execution order.
/// </summary>
public class HandlerStepModel
{
    /// <summary>
    ///     Gets or sets the name of the handler step.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    ///     Gets or sets the execution order of the handler step.
    ///     Lower values execute first.
    /// </summary>
    public int Order { get; set; }
}