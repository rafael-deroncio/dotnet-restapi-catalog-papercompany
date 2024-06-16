namespace PapperCompany.Catalog.Domain.Responses;

public class TokenResponse
{
    public string Token { get; set; }
    public DateTime Expires { get; set; }
    public string Message { get; set; }
}
