using PapperCompany.Catalog.API.Settings;

namespace PapperCompany.Catalog.API.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseSwagger(this IApplicationBuilder builder, IConfiguration configuration)
    {
        // Load Swagger settings from appsettings.json
        SwaggerSettings swaggerSettings = configuration.GetSection("SwaggerSettings").Get<SwaggerSettings>()
            ?? throw new NullReferenceException("No settings for swagger documentation were found.");

        // Configure Swagger UI with custom settings
        builder.UseSwaggerUI(options =>
        {
            // Set the default models expand depth
            options.DefaultModelsExpandDepth(-1);

            // Configure the Swagger endpoint using the title from settings
            options.SwaggerEndpoint($"/swagger/{swaggerSettings.Name}/swagger.json", swaggerSettings.Title);
        });

        builder.UseSwagger();

        return builder;
    }
}
