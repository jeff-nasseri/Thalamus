using Application.Common.Consts;
using Application.Common.ErrorMessaging;
using ErrorHandling.Attributes;
using ErrorHandling.Enums;
using ErrorHandling.Helpers;

namespace Application.Common.Util;

/// <summary>
///     Provides utility methods for generating structured logging tags and metadata.
/// </summary>
public class LoggerUtils
{
    /// <summary>
    ///     Generates a dictionary of required error logging tags for structured logging.
    /// </summary>
    /// <param name="enum">The error or point of failure enum.</param>
    /// <param name="backendErrorType">The backend error type classification.</param>
    /// <param name="teams">The teams responsible for the error.</param>
    /// <param name="groups">The error groups for categorization.</param>
    /// <returns>A dictionary of logging tags with error metadata.</returns>
    public static IDictionary<string, object> GenerateRequiredErrorLogTags(Enum @enum,
        BackendErrorType backendErrorType,
        IEnumerable<ErrorResponsibilityTeam> teams,
        IEnumerable<ServerErrorGroup> groups)
    {
        IDictionary<string, object> dict = new Dictionary<string, object>();

        var data =
            teams.Select(EnumHelper<ErrorResponsibilityDetailAttribute, ErrorResponsibilityTeam>.GetCustomAttribute);

        var teamStr = string.Join(',', data.Select(d => d.Title));
        dict.Add(ErrorLoggerKeys.RESPONSIBLE_TEAMS, teamStr);

        var names = groups.Select(g => g.GetName());
        var groupTagStr = string.Join(", ", names);

        dict.Add(ErrorLoggerKeys.ERROR_GROUPS, groupTagStr);
        dict.Add(ErrorLoggerKeys.ERROR_TYPE, backendErrorType);

        SetupErrorTags(ref dict, @enum);
        SetupPofTags(ref dict, @enum);

        return dict;
    }

    /// <summary>
    ///     Gets the error description from resource files.
    /// </summary>
    /// <param name="enum">The error enum.</param>
    /// <returns>The error description string.</returns>
    private static string GetDescription(Enum @enum)
    {
        return ErrorMessageUtils.GetErrorMessage(@enum);
    }

    /// <summary>
    ///     Adds Point of Failure (POF) specific tags to the logging dictionary.
    /// </summary>
    /// <param name="dict">The logging tags dictionary.</param>
    /// <param name="enum">The error enum to check for POF.</param>
    private static void SetupPofTags(ref IDictionary<string, object> dict, Enum @enum)
    {
        if (@enum is not PointOfFailure pof) return;

        dict.Add(ErrorLoggerKeys.POF, pof);

        var code = ErrorCodeHelper.Format(Convert.ToInt32(@enum));
        dict.Add(ErrorLoggerKeys.POF_CODE, code);

        dict.Add(ErrorLoggerKeys.POF_EXPLANATION, GetDescription(@enum));
    }

    /// <summary>
    ///     Adds error-specific tags to the logging dictionary.
    /// </summary>
    /// <param name="dict">The logging tags dictionary.</param>
    /// <param name="enum">The error enum.</param>
    private static void SetupErrorTags(ref IDictionary<string, object> dict, Enum @enum)
    {
        if (@enum is PointOfFailure) return;

        dict.Add(ErrorLoggerKeys.ERROR, @enum);

        var code = ErrorCodeHelper.Format(Convert.ToInt32(@enum));
        dict.Add(ErrorLoggerKeys.ERROR_CODE, code);

        dict.Add(ErrorLoggerKeys.ERROR_EXPLANATION, GetDescription(@enum));
    }
}