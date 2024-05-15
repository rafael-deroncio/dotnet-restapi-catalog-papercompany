using PapperCompany.Catalog.API.Settings;
using YamlDotNet.Serialization;

namespace PapperCompany.Catalog.API.Configurations;

public static class SecretsConfigurations
{
    public static SecretsSettings Get()
    {
        try
        {
            using StreamReader reader = new(@"./secrets.yaml");
            return new DeserializerBuilder().Build().Deserialize<SecretsSettings>(reader);
        }
        catch (Exception ex)
        {
            throw new FileLoadException($"Unable to get system secrets :{ex}");
        }
    }
}
