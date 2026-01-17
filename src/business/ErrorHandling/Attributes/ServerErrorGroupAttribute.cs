using ErrorHandling.Enums;

namespace ErrorHandling.Attributes;

/// <summary>
///     Attribute to categorize errors into server error groups.
///     Can be applied multiple times to assign an error to multiple groups.
/// </summary>
[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
public class ServerErrorGroupAttribute : Attribute
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ServerErrorGroupAttribute" /> class.
    /// </summary>
    /// <param name="group">The server error group to assign.</param>
    public ServerErrorGroupAttribute(ServerErrorGroup group)
    {
        ServerErrorGroup = group;
    }

    /// <summary>
    ///     Gets or sets the server error group classification.
    /// </summary>
    public ServerErrorGroup ServerErrorGroup { get; set; }
}