
/*
 * Firewall System Architecture:
 * 1. Commands/Queries: All requests are categorized as either commands or queries
 * 2. Queries: Check the permission of the request author, if valid allow execution
 * 3. Commands: Check the permission of the request author, if valid allow execution
 * 4. Rules are applied based on the firewall rule category and cushion level
 */

/// <summary>
/// Attribute to specify required permissions for a request.
/// Can be applied multiple times to require multiple permissions.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RequestPermissionAttribute : Attribute
{
    /// <summary>
    /// Gets or sets the required permission level.
    /// </summary>
    public Permissions Permission { get; set; }
}