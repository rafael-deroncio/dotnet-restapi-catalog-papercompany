using Microsoft.AspNetCore.Mvc.Versioning;
using PapperCompany.Catalog.API.Settings;

namespace PapperCompany.Catalog.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCors(this IServiceCollection services, IConfiguration configuration)
    {
        // Load CORS settings from the provided configuration
        CorsPolicy policy = configuration.GetSection("CorsSettings").Get<CorsSettings>().Policy
            ?? throw new NullReferenceException("No settings for cors were found.");

        services.AddCors(options =>
        {
            options.AddPolicy(policy.Name, builder =>
            {
                builder.WithHeaders(policy.AllowedHeaders);
                builder.WithMethods(policy.AllowedMethods);

                if (policy.AllowedOrigins.Contains("*")) builder.AllowAnyOrigin();
                else builder.WithOrigins(policy.AllowedOrigins);
            });
        });

        return services;
    }

    public static IServiceCollection AddVersioning(this IServiceCollection services, IConfiguration configuration)
    {
        VersioningSettings settings = configuration.GetSection("VersioningSettings").Get<VersioningSettings>()
           ?? throw new NullReferenceException("No settings for API versioning were found.");

        services.AddApiVersioning(options =>
        {
            // Set the default API version and assume it when not specified
            options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(
                settings.ApiVersion.High,
                settings.ApiVersion.Medium);

            options.AssumeDefaultVersionWhenUnspecified = true;

            // Report API versions in response headers
            options.ReportApiVersions = true;

            // Choose the API version based on the current implementation
            options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);

            // Read API versions from URL segments, headers, and media types
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader(settings.Reader),
                new MediaTypeApiVersionReader(settings.Reader)
            );
        });

        services.AddVersionedApiExplorer(setup =>
        {
            // Format the group name using the API version in the 'vVVV' format
            setup.GroupNameFormat = settings.Explorer.Format;

            // Substitute the API version in URLs
            setup.SubstituteApiVersionInUrl = true;
        });

        return services;
    }
}
