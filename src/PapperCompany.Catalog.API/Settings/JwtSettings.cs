namespace PapperCompany.Catalog.API.Settings;

public class JwtSettings
{
    public byte[] IssuerSigningKey { get; set; }
    public bool ValidateIssuerSigningKey { get; set; }
    public bool ValidateIssuer { get; set; }
    public bool ValidateAudience { get; set; }
    public bool RequireHttpsMetadata { get; set; }
    public bool SaveToken { get; set; }
}
