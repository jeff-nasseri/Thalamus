namespace Application.Common.Consts;

/// <summary>
///     Contains constant keys used for error logging throughout the application.
/// </summary>
public abstract class ErrorLoggerKeys
{
    /// <summary>
    ///     The key for responsible teams in error logs.
    /// </summary>
    public const string RESPONSIBLE_TEAMS = "Responsible-Teams";

    /// <summary>
    ///     The key for error groups in error logs.
    /// </summary>
    public const string ERROR_GROUPS = "Error-Groups";

    /// <summary>
    ///     The key for error type in error logs.
    /// </summary>
    public const string ERROR_TYPE = "Error-Type";

    /// <summary>
    ///     The key for error in error logs.
    /// </summary>
    public const string ERROR = "Error";

    /// <summary>
    ///     The key for error code in error logs.
    /// </summary>
    public const string ERROR_CODE = "Error-Code";

    /// <summary>
    ///     The key for error explanation in error logs.
    /// </summary>
    public const string ERROR_EXPLANATION = "Error-Explain";

    /// <summary>
    ///     The key for Point of Failure (POF) in error logs.
    /// </summary>
    public const string POF = "POF";

    /// <summary>
    ///     The key for Point of Failure code in error logs.
    /// </summary>
    public const string POF_CODE = "POF-code";

    /// <summary>
    ///     The key for Point of Failure explanation in error logs.
    /// </summary>
    public const string POF_EXPLANATION = "POF-Explain";
}