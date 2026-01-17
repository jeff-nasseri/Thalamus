/// <summary>
///     Represents permission levels for incoming requests to the application layer.
///     Follows Unix-like chmod permission structure for access control validation by the firewall pipeline behavior.
/// </summary>
public enum Permissions
{
    /// <summary>
    ///     Read-only access permission (similar to Unix r/4).
    /// </summary>
    READ = 4,

    /// <summary>
    ///     Write access permission (similar to Unix w/2).
    /// </summary>
    WRITE = 2,

    /// <summary>
    ///     Execute access permission (similar to Unix x/1).
    /// </summary>
    EXECUTE = 1,

    /// <summary>
    ///     Full access permission (read, write, and execute - similar to Unix 7).
    /// </summary>
    ALL = 7
}