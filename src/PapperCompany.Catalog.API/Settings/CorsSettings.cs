namespace PapperCompany.Catalog.API.Settings;

public class CorsSettings
{
    public CorsPolicy Policy { get; set; }
}

public class CorsPolicy
{
    public string Name { get; set; }
    public string[] AllowedOrigins { get; set; }
    public string[] AllowedMethods { get; set; }
    public string[] AllowedHeaders { get; set; }
}
