using PapperCompany.Catalog.API.Configurations;
using PapperCompany.Catalog.API.Settings;

namespace PapperCompany.Catalog.API.Extensions;

public static class ConfigurationExtensions
{
    public static IConfigurationBuilder AddSecrets(this IConfigurationBuilder builder)
    {
        SecretsSettings secrets = SecretsConfigurations.Get();

        builder.AddInMemoryCollection(new Dictionary<string, string>
        {
            {"CatalogConnectionString", secrets.CatalogConnectionString},
            {"JwtSymmetricSecurityKey", secrets.JwtSymmetricSecurityKey},
        });

        return builder;
    }
}
