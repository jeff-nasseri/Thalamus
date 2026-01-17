using Application.Common.Validators;
using Infrastructure.DependencyInjection;
using Infrastructure.Services.Ip;
using Infrastructure.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Thalamus.Web.Logging.Sentry;
using Thalamus.Web.Swagger;
using DependencyInjection = Application.DependencyInjection;

namespace Thalamus.Web.Extensions;

/// <summary>
///     Provides extension methods for configuring application services during startup.
/// </summary>
public static class StartUpExtensions
{
    /// <summary>
    ///     Configures custom Swagger documentation services with client-based API description provider.
    /// </summary>
    /// <param name="services">Service collection to configure.</param>
    /// <returns>The modified service collection.</returns>
    public static IServiceCollection SetupCustomSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen();
        services.AddCustomSwagger();
        services.TryAddEnumerable(ServiceDescriptor
            .Transient<IApiDescriptionProvider, ClientBasedApiDescriptionProvider>());
        return services;
    }

    /// <summary>
    ///     Configures data protection services with AES-256-CBC encryption and HMACSHA256 validation.
    /// </summary>
    /// <param name="services">Service collection to configure.</param>
    /// <returns>The modified service collection.</returns>
    public static IServiceCollection SetupDataProtection(this IServiceCollection services)
    {
        services.AddDataProtection().UseCryptographicAlgorithms(
            new AuthenticatedEncryptorConfiguration
            {
                EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
                ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
            });

        return services;
    }

    /// <summary>
    ///     Configures MVC controllers with API versioning and endpoints explorer.
    /// </summary>
    /// <param name="services">Service collection to configure.</param>
    /// <returns>The modified service collection.</returns>
    public static IServiceCollection SetupControllers(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
            options.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
        });

        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        services.AddHttpContextAccessor();

        return services;
    }

    /// <summary>
    ///     Configures MediatR with request validators and module registries.
    /// </summary>
    /// <param name="services">Service collection to configure.</param>
    /// <returns>The modified service collection.</returns>
    public static IServiceCollection SetupMediateR(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRequestValidator<>), typeof(RequestValidator<>));
        services.ModuleRegistry(typeof(DependencyInjection).Assembly,
            typeof(IpService).Assembly);

        return services;
    }

    /// <summary>
    ///     Configures Swagger UI middleware for API documentation.
    /// </summary>
    /// <param name="app">Application builder to configure.</param>
    /// <returns>The modified application builder.</returns>
    public static IApplicationBuilder SetupSwaggerUi(this IApplicationBuilder app)
    {
        var apiDescriptionGroupCollectionProvider =
            app.ApplicationServices.GetRequiredService<IApiDescriptionGroupCollectionProvider>();
        app.UseCustomSwaggerUi(apiDescriptionGroupCollectionProvider);

        return app;
    }

    /// <summary>
    ///     Configures Keycloak authentication with default options.
    /// </summary>
    /// <param name="services">Service collection to configure.</param>
    /// <returns>The modified service collection.</returns>
    public static IServiceCollection SetupKeycloak(this IServiceCollection services)
    {
        services.SetupKeycloak(_ => { });

        return services;
    }

    /// <summary>
    ///     Configures Keycloak authentication with custom authorization options.
    /// </summary>
    /// <param name="services">Service collection to configure.</param>
    /// <param name="configure">Action to configure authorization options.</param>
    /// <returns>The modified service collection.</returns>
    public static IServiceCollection SetupKeycloak(this IServiceCollection services,
        Action<AuthorizationOptions> configure)
    {
        return services;
    }

    /// <summary>
    ///     Registers custom Sentry event processor for enhanced error tracking.
    /// </summary>
    /// <param name="services">Service collection to configure.</param>
    /// <returns>The modified service collection.</returns>
    public static IServiceCollection SetupSentryCustomisation(this IServiceCollection services)
    {
        services.AddSingleton<CustomSentryEventProcessor>();
        return services;
    }
}