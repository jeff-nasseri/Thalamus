using System.Reflection;
using ErrorHandling.Attributes;
using ErrorHandling.Enums;
using ErrorHandling.Exceptions;

namespace ErrorHandling.Helpers;

/// <summary>
/// Helper class for error type conversion and categorization.
/// </summary>
public static class ErrorTypeHelper
{
    /// <summary>
    /// Gets the backend error type for the given error code enum value.
    /// </summary>
    /// <param name="errorCode">The error code enum value.</param>
    /// <returns>The <see cref="BackendErrorType"/> associated with the error code.</returns>
    /// <exception cref="ErrorTypeAttributeIsMissingException">
    /// Thrown when the error code is not annotated with <see cref="ErrorTypeAttribute" />.
    /// </exception>
    public static BackendErrorType GetBackendErrorType(Enum errorCode)
    {
        Type enumType = errorCode.GetType();
        FieldInfo fieldInfo = enumType.GetField(errorCode.ToString())!;
        ErrorTypeAttribute errorTypeAttribute = fieldInfo.GetCustomAttribute<ErrorTypeAttribute>()
                                                ?? throw new ErrorTypeAttributeIsMissingException(errorCode);

        return errorTypeAttribute.BackendErrorType;
    }

    /// <summary>
    /// Gets the client error type for the given error code enum value.
    /// </summary>
    /// <param name="errorCode">The error code enum value.</param>
    /// <returns>The <see cref="ClientErrorType"/> mapped from the backend error type.</returns>
    /// <exception cref="ErrorTypeAttributeIsMissingException">
    /// Thrown when the error code is not annotated with <see cref="ErrorTypeAttribute" />.
    /// </exception>
    public static ClientErrorType GetClientErrorType(Enum errorCode)
    {
        return GetClientErrorType(GetBackendErrorType(errorCode));
    }

    /// <summary>
    /// Converts a backend error type to its corresponding client error type.
    /// </summary>
    /// <param name="backendErrorType">The backend error type to convert.</param>
    /// <returns>The corresponding <see cref="ClientErrorType"/>.</returns>
    public static ClientErrorType GetClientErrorType(BackendErrorType backendErrorType)
    {
        return backendErrorType switch
        {
            BackendErrorType.BusinessLogic => ClientErrorType.BusinessLogic,
            BackendErrorType.SecurityAttempt => ClientErrorType.BusinessLogic,
            _ => ClientErrorType.InternalServerError
        };
    }

    /// <summary>
    /// Gets the server error groups for an error code enum value.
    /// Groups can be modified using <see cref="ServerErrorGroupAttribute"/>.
    /// </summary>
    /// <param name="enum">The error code enum value.</param>
    /// <returns>A collection of <see cref="ServerErrorGroup"/> values.</returns>
    public static IEnumerable<ServerErrorGroup> GetErrorGroups(Enum @enum)
    {
        IEnumerable<ServerErrorGroupAttribute> result = EnumHelper<ServerErrorGroupAttribute, Enum>.GetCustomAttributes(@enum);

        if (result.Any())
        {
            return result.Select(i => i.ServerErrorGroup);
        }

        IEnumerable<ErrorTypeAttribute> errorTypes = EnumHelper<ErrorTypeAttribute, Enum>.GetCustomAttributes(@enum);

        foreach (ErrorTypeAttribute errorType in errorTypes)
        {
            BackendErrorType backendErrorType = errorType.BackendErrorType;
            result = EnumHelper<ServerErrorGroupAttribute, Enum>.GetCustomAttributes(backendErrorType);
        }

        return result.Select(i => i.ServerErrorGroup);
    }

    /// <summary>
    /// Gets comprehensive error information including type, groups, and responsible teams.
    /// </summary>
    /// <param name="enum">The error code enum value.</param>
    /// <returns>A tuple containing backend error type, server error groups, and responsible teams.</returns>
    public static (BackendErrorType Type, IEnumerable<ServerErrorGroup> Groups, IEnumerable<ErrorResponsibilityTeam> Teams)
        ModifyError(Enum @enum)
    {
        IEnumerable<ServerErrorGroup> groups = GetErrorGroups(@enum);
        IEnumerable<ErrorResponsibilityTeam> teams = groups.SelectMany(ErrorResponsibilityHelper.GetResponsibleTeams);
        BackendErrorType backendErrorType = GetBackendErrorType(@enum);

        return (backendErrorType, groups, teams);
    }
}