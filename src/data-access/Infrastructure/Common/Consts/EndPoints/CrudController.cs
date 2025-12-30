namespace Infrastructure.Common.Consts.EndPoints;

/// <summary>
/// Defines standard CRUD endpoint constants for controller routes.
/// </summary>
public abstract class CrudController
{
    /// <summary>
    /// Endpoint constant for create operations.
    /// </summary>
    public const string CREATE = "create";

    /// <summary>
    /// Endpoint constant for update operations.
    /// </summary>
    public const string UPDATE = "update";

    /// <summary>
    /// Endpoint constant for delete operations.
    /// </summary>
    public const string DELETE = "delete";

    /// <summary>
    /// Endpoint constant for list operations.
    /// </summary>
    public const string LIST = "list";

    /// <summary>
    /// Endpoint constant for get operations.
    /// </summary>
    public const string GET = "get";

    /// <summary>
    /// Endpoint constant for create or update (upsert) operations.
    /// </summary>
    public const string CREATE_OR_UPDATE = "create-or-update";

    /// <summary>
    /// Endpoint constant for assignee confirmation operations.
    /// </summary>
    public const string ASSIGNEE_CONFIRMATION = "assignee-confirmation";
}