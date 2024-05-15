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
}
