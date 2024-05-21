using System.Text.Json.Serialization;

namespace PapperCompany.Catalog.Domain.Requests;

public class PaginationRequest : Request
{
    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("size")]
    public int Size { get; set; }
}
