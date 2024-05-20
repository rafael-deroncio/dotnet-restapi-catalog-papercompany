using PapperCompany.Catalog.Core.Configurations.Secrets;

namespace PapperCompany.Catalog.API.Extensions;

public static class ConfigurationExtensions
{
    public static IConfigurationBuilder AddSecrets(this IConfigurationBuilder builder)
    {
        string file = "secrets.yaml";

        Secrets secrets = SecretsReader.Get(file);

        builder.AddInMemoryCollection(new Dictionary<string, string>
        {
            {"DBCatalogConnectionString", secrets.DBCatalogConnectionString},
            {"JwtSymmetricSecurityKey", secrets.JwtSymmetricSecurityKey},
        });

        return builder;
    }
}
