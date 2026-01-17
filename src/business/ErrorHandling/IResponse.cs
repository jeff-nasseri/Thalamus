namespace ErrorHandling;

/// <summary>
///     Represents a response that indicates success or failure with an optional error.
/// </summary>
public interface IResponse
{
    /// <summary>
    ///     Gets a value indicating whether the operation was successful.
    /// </summary>
    public bool Success { get; init; }

    /// <summary>
    ///     Gets the error information if the operation failed.
    /// </summary>
    public Error? Error { get; init; }
}

/// <summary>
///     Represents a response with typed data that indicates success or failure with an optional error.
/// </summary>
/// <typeparam name="TData">The type of data returned on success.</typeparam>
public interface IResponse<out TData> : IResponse
{
    /// <summary>
    ///     Gets the data returned by the operation.
    /// </summary>
    public TData? Data { get; }
}