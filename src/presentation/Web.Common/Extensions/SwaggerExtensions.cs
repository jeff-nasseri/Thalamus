using Thalamus.Web.Swagger;
#if !DEBUG
using Infrastructure.Common;
using Infrastructure.Settings.Swagger;
#endif
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;

namespace Thalamus.Web.Extensions;

/// <summary>
/// Provides extension methods for configuring Swagger documentation.
/// </summary>
public static class SwaggerExtensions
{
    private const string SWAGGER_PREFIX_PATH = "_sw";

    /// <summary>
    /// Adds custom Swagger generation services with configured options.
    /// </summary>
    /// <param name="services">Service collection to configure.</param>
    /// <returns>The modified service collection.</returns>
    public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen();
        services.ConfigureOptions<ConfigureSwaggerOptions>();
        return services;
    }

    /// <summary>
    /// Configures custom Swagger UI middleware with basic authentication in non-debug builds.
    /// </summary>
    /// <param name="app">Application builder to configure.</param>
    /// <param name="apiDescriptionGroupCollectionProvider">API description group collection provider.</param>
    /// <returns>The modified application builder.</returns>
    public static IApplicationBuilder UseCustomSwaggerUi(
        this IApplicationBuilder app,
        IApiDescriptionGroupCollectionProvider apiDescriptionGroupCollectionProvider)
    {
        app.UseWhen(context => context.Request.Path.StartsWithSegments($"/{SWAGGER_PREFIX_PATH}"), appBuilder =>
        {
#if !DEBUG
            SwaggerEnvironmentSetting setting = EnvUtils.GetEnvironment<SwaggerEnvironmentSetting>();
            appBuilder.UseMiddleware<SwaggerBasicAuthMiddleware>(setting.Username, setting.Password);
#endif
        });

        app.UseSwagger(options =>
        {
            options.RouteTemplate = $"{SWAGGER_PREFIX_PATH}/{{documentName}}/swagger.json";
        });
        app.UseSwaggerUI(options =>
        {
            options.RoutePrefix = SWAGGER_PREFIX_PATH;

            foreach (string groupName in apiDescriptionGroupCollectionProvider.ApiDescriptionGroups.Items.Select(a => a.GroupName!))
            {
                options.SwaggerEndpoint($"/{SWAGGER_PREFIX_PATH}/{groupName}/swagger.json", groupName);
            }

            options.EnableDeepLinking();
        });
        return app;
    }
}