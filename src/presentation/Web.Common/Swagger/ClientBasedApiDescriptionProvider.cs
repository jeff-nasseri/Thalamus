using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;

//Resharper disable all

namespace Thalamus.Web.Swagger;

/// <summary>
/// Custom API description provider that organizes API endpoints by version.
/// </summary>
public class ClientBasedApiDescriptionProvider : IApiDescriptionProvider
{
    private readonly IOptions<ApiExplorerOptions> _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="ClientBasedApiDescriptionProvider"/> class.
    /// </summary>
    /// <param name="options">API explorer options for configuration.</param>
    public ClientBasedApiDescriptionProvider(IOptions<ApiExplorerOptions> options)
    {
        _options = options;
    }

    /// <summary>
    /// Gets the order in which this provider is executed. Lower values execute first.
    /// </summary>
    public int Order => -1;

    /// <summary>
    /// Called during the executing phase of API description generation.
    /// </summary>
    /// <param name="context">The context for API description providers.</param>
    public void OnProvidersExecuting(ApiDescriptionProviderContext context)
    {
    }

    /// <summary>
    /// Called after all providers have executed. Assigns group names based on API version.
    /// </summary>
    /// <param name="context">The context for API description providers.</param>
    public void OnProvidersExecuted(ApiDescriptionProviderContext context)
    {
        string format = _options.Value.GroupNameFormat;
        CultureInfo culture = CultureInfo.CurrentCulture;
        IList<ApiDescription> results = context.Results;
        List<ApiDescription> sharedApiDescriptions = new();

        foreach (ApiDescription apiDescription in results)
        {
            ApiVersion apiVersion = apiDescription.GetApiVersion();
            string version = apiVersion.ToString(format, culture);
            apiDescription.GroupName = version;
        }

        sharedApiDescriptions.ForEach(results.Add);
    }
}