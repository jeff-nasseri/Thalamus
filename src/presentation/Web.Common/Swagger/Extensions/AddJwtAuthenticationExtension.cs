using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Thalamus.Web.Swagger.Extensions;

/// <summary>
///     Provides extension methods for configuring JWT authentication in Swagger.
/// </summary>
public static class AddJwtAuthenticationExtension
{
    /// <summary>
    ///     Configures Swagger to support JWT Bearer authentication.
    /// </summary>
    /// <param name="services">Service collection to configure.</param>
    /// <returns>The modified service collection.</returns>
    public static IServiceCollection ConfigureSwaggerJwtAuthentication(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            OpenApiSecurityScheme securityScheme = new()
            {
                Name = "JWT Authentication",
                Description = "Enter JWT Bearer token **_only_**",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };
            c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { securityScheme, Array.Empty<string>() }
            });
        });
        return services;
    }
}