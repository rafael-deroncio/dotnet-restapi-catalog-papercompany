using YamlDotNet.Serialization;

namespace PapperCompany.Catalog.Core.Configurations.Secrets;

public class SecretsReader
{
    public static Secrets Get(string file)
    {
        try
        {
            using StreamReader reader = new(string.Format(@"./{0}", file));
            return new DeserializerBuilder().Build().Deserialize<Secrets>(reader);
        }
        catch (Exception ex)
        {
            throw new FileLoadException($"Unable to get system secrets :{ex}");
        }
    }
}
