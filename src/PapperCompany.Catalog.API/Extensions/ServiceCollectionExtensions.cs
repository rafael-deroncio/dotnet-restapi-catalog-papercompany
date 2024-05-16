using System.Text;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using PapperCompany.Catalog.API.Settings;
using Microsoft.IdentityModel.Tokens;
using PapperCompany.Catalog.Core.Services.Interfaces;
using PapperCompany.Catalog.Core.Services;

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

    public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        // Load SwaggerGenDocSettings from appsettings.json
        SwaggerSettings swaggerSettings = configuration.GetSection("SwaggerSettings").Get<SwaggerSettings>()
            ?? throw new NullReferenceException("No settings for swagger documentation were found.");

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(swaggerSettings.Name, new OpenApiInfo
            {
                Title = swaggerSettings.Title,
                Version = swaggerSettings.Version,
                Description = swaggerSettings.Description,
                Contact = new OpenApiContact
                {
                    Name = swaggerSettings.Contact.Name,
                    Email = swaggerSettings.Contact.Email,
                    Url = new Uri(swaggerSettings.Contact.Url)
                }
            });

            //var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        return services;
    }

    public static IServiceCollection AddSwaggerJwtBearer(this IServiceCollection services, IConfiguration configuration)
    {
        // Load Swagger JWT Bearer settings from the provided configuration
        SwaggerJwtBearerSettings settings = configuration.GetSection("SwaggerJwtBearerSettings").Get<SwaggerJwtBearerSettings>()
            ?? throw new NullReferenceException("No settings for swagger jwt bearer were found.");

        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition(settings.Scheme, new OpenApiSecurityScheme
            {
                Name = settings.Name,
                Type = SecuritySchemeType.ApiKey,
                Scheme = settings.Scheme,
                BearerFormat = settings.BearerFormat,
                In = ParameterLocation.Header,
                Description = settings.Description,
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement{
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = settings.Scheme
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        return services;
    }

    public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        byte[] key = Encoding.UTF8.GetBytes(configuration["JwtSymmetricSecurityKey"]) ??
            throw new NullReferenceException("No settings for jwt securit key were found.");

        JwtSettings settings = configuration.GetSection("JwtSettings").Get<JwtSettings>()
            ?? throw new NullReferenceException("No settings for jwt were found.");

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = settings.RequireHttpsMetadata;
            options.SaveToken = settings.SaveToken;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = settings.ValidateIssuer,
                ValidateIssuerSigningKey = settings.ValidateIssuerSigningKey,
                ValidateAudience = settings.ValidateAudience
            };
        });

        return services;
    }

    public static IServiceCollection AddLowerCaseRouting(this IServiceCollection services)
    {
        services.Configure<RouteOptions>(options =>
        {
            options.LowercaseUrls = true;
        });

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services;
    }
}
