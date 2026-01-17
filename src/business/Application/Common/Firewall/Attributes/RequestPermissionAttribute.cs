/// <summary>
///     Attribute to specify required permissions for a request.
///     Can be applied multiple times to require multiple permissions.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RequestPermissionAttribute : Attribute
{
    /// <summary>
    ///     Gets or sets the required permission level.
    /// </summary>
    public Permissions Permission { get; set; }
}