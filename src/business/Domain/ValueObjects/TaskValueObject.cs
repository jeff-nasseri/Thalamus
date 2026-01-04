using Domain.Common.BaseTypes;

namespace Domain.ValueObjects;

/// <summary>
///     Represents a task value object within a plan.
///     A plan can contain one or more tasks that need to be executed by agents.
/// </summary>
public class TaskValueObject : ValueObject
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="TaskValueObject" /> class.
    /// </summary>
    public TaskValueObject()
    {
        Name = string.Empty;
        Description = string.Empty;
        IsDone = false;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="TaskValueObject" /> class with specified values.
    /// </summary>
    /// <param name="order">The execution order of the task.</param>
    /// <param name="name">The name of the task.</param>
    /// <param name="description">The description of the task.</param>
    public TaskValueObject(int order, string name, string description)
    {
        Order = order;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description ?? throw new ArgumentNullException(nameof(description));
        IsDone = false;
    }

    /// <summary>
    ///     Gets or sets the execution order of the task within the plan.
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    ///     Gets or sets the name of the task.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    ///     Gets or sets the description of the task.
    /// </summary>
    public string Description { get; set; } = null!;

    /// <summary>
    ///     Gets or sets a value indicating whether the task has been completed.
    /// </summary>
    public bool IsDone { get; set; }

    /// <summary>
    ///     Marks the task as completed.
    /// </summary>
    public void MarkAsCompleted()
    {
        IsDone = true;
    }

    /// <summary>
    ///     Marks the task as not completed.
    /// </summary>
    public void MarkAsIncomplete()
    {
        IsDone = false;
    }

    /// <summary>
    ///     Gets the components that define the equality of the TaskValueObject.
    /// </summary>
    /// <returns>An enumerable of objects that represent the equality components.</returns>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Order;
        yield return Name;
        yield return Description;
    }
}