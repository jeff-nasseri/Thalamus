namespace ErrorHandling;

/// <summary>
///     Represents a non-generic response structure that indicates success or failure.
/// </summary>
public class Response : IResponse
{
    protected Response(Error error) : this()
    {
        Success = false;
        Error = error;
    }

    protected Response()
    {
    }

    /// <summary>
    ///     Gets or initializes the optional data associated with the response.
    /// </summary>
    public object? Data { get; init; } = null;

    /// <summary>
    ///     Gets or initializes a value indicating whether the operation was successful.
    /// </summary>
    public bool Success { get; init; }

    /// <summary>
    ///     Gets or initializes the error information if the operation failed.
    /// </summary>
    public Error? Error { get; init; }

    /// <summary>
    ///     Implicitly converts an <see cref="Error" /> to a failed <see cref="Response" />.
    /// </summary>
    /// <param name="error">The error to convert.</param>
    public static implicit operator Response(Error error)
    {
        return new Response(error);
    }

    /// <summary>
    ///     Implicitly converts an error code enum to a failed <see cref="Response" />.
    /// </summary>
    /// <param name="code">The error code enum to convert.</param>
    public static implicit operator Response(Enum code)
    {
        return new Response(ErrorHandling.Error.FromErrorCode(code));
    }

    /// <summary>
    ///     Creates a successful response.
    /// </summary>
    /// <returns>A new <see cref="Response" /> indicating success.</returns>
    public static Response Successful()
    {
        return new Response { Success = true };
    }

    /// <summary>
    ///     Attempts to get the error from the response.
    /// </summary>
    /// <param name="error">The error if present; otherwise, null.</param>
    /// <returns>True if an error exists; otherwise, false.</returns>
    public bool TryGetError(out Error? error)
    {
        error = Error;
        return error != null;
    }
}

/// <summary>
///     Represents a generic response structure that contains typed data and indicates success or failure.
/// </summary>
/// <typeparam name="TData">The type of data returned on success.</typeparam>
public class Response<TData> : IResponse<TData>
{
    protected Response(Error error)
    {
        Success = false;
        Error = error;
    }

    protected Response(TData? value)
    {
        Success = true;
        Data = value;
    }

    /// <summary>
    ///     Gets or initializes a value indicating whether the operation was successful.
    /// </summary>
    public bool Success { get; init; }

    /// <summary>
    ///     Gets the data returned by the operation.
    /// </summary>
    public TData? Data { get; }

    /// <summary>
    ///     Gets or initializes the error information if the operation failed.
    /// </summary>
    public Error? Error { get; init; }

    /// <summary>
    ///     Implicitly converts typed data to a successful <see cref="Response{TData}" />.
    /// </summary>
    /// <param name="value">The data value to convert.</param>
    public static implicit operator Response<TData>(TData value)
    {
        return new Response<TData>(value);
    }

    /// <summary>
    ///     Implicitly converts an <see cref="Error" /> to a failed <see cref="Response{TData}" />.
    /// </summary>
    /// <param name="error">The error to convert.</param>
    public static implicit operator Response<TData>(Error error)
    {
        return new Response<TData>(error);
    }

    /// <summary>
    ///     Implicitly converts an error code enum to a failed <see cref="Response{TData}" />.
    /// </summary>
    /// <param name="code">The error code enum to convert.</param>
    public static implicit operator Response<TData>(Enum code)
    {
        return new Response<TData>(ErrorHandling.Error.FromErrorCode(code));
    }
}