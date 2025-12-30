using ErrorHandling.Enums;

namespace ErrorHandling.Attributes;

/// <summary>
/// Attribute to mark classes or methods with their potential points of failure.
/// Can be applied multiple times to document multiple failure scenarios.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class PointOfFailureAttribute : Attribute
{
    /// <summary>
    /// Gets or sets the array of point of failure scenarios.
    /// </summary>
    public PointOfFailure[] PointOfFailures { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PointOfFailureAttribute"/> class.
    /// </summary>
    /// <param name="pointOfFailures">One or more points of failure that may occur.</param>
    public PointOfFailureAttribute(params PointOfFailure[] pointOfFailures)
    {
        PointOfFailures = pointOfFailures;
    }
}