using Domain.Common.BaseTypes;

namespace Domain.ValueObjects;

/// <summary>
///     Represents the result of a plan execution, containing request-response pairs and metadata.
/// </summary>
public class PlanResultValueObject : ValueObject
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="PlanResultValueObject" /> class.
    /// </summary>
    public PlanResultValueObject()
    {
        Title = string.Empty;
        Pairs = new List<(object request, object response)>();
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="PlanResultValueObject" /> class with a title.
    /// </summary>
    /// <param name="title">The title of the plan result.</param>
    public PlanResultValueObject(string title)
    {
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Pairs = new List<(object request, object response)>();
    }

    /// <summary>
    ///     Gets or sets the title of the plan result.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    ///     Gets or sets the collection of request-response pairs from the plan execution.
    ///     Each pair represents a request sent during execution and its corresponding response.
    /// </summary>
    public ICollection<(object request, object response)> Pairs { get; set; }

    /// <summary>
    ///     Adds a request-response pair to the result.
    /// </summary>
    /// <param name="request">The request object.</param>
    /// <param name="response">The response object.</param>
    public void AddPair(object request, object response)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        if (response == null) throw new ArgumentNullException(nameof(response));

        Pairs.Add((request, response));
    }

    /// <summary>
    ///     Gets the components that define the equality of the PlanResultValueObject.
    /// </summary>
    /// <returns>An enumerable of objects that represent the equality components.</returns>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Title;

        foreach (var pair in Pairs)
        {
            yield return pair.request;
            yield return pair.response;
        }
    }
}