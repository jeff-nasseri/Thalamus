using System.Globalization;
using System.Resources;
using Application.Common.Consts;
using ErrorHandling.Helpers;

namespace Application.Common.ErrorMessaging;

/// <summary>
/// Provides utilities for retrieving error messages from resource files.
/// </summary>
public static class ErrorMessageUtils
{
    /// <summary>
    /// Gets the error message for an enum error code from application resource files.
    /// </summary>
    /// <typeparam name="TEnum">The enum type representing the error code.</typeparam>
    /// <param name="enum">The error code enum value.</param>
    /// <returns>The error message string, or NO-DESCRIPTION-FOUND if not found.</returns>
    public static string GetErrorMessage<TEnum>(TEnum @enum) where TEnum : Enum
    {
        string value = ErrorCodeHelper.Format(Convert.ToInt32(@enum));

        IEnumerable<Type> resources = typeof(IApplicationDomainMarkup).Assembly.GetTypes()
            .Where(t => t.FullName!.EndsWith(ApplicationKeys.RESOURCE_FILE_SUFFIX));

        foreach (Type resource in resources)
        {
            ResourceManager resourceManager = new(resource.FullName!, resource.Assembly);
            string result = resourceManager.GetString(value, CultureInfo.CurrentCulture)!;

            if (string.IsNullOrEmpty(result))
            {
                continue;
            }

            return result;
        }

        return ApplicationKeys.NO_DESCRIPTION_FOUND;
    }
}