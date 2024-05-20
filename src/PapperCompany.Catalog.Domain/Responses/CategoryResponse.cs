using System.Text.Json.Serialization;

namespace PapperCompany.Catalog.Domain.Responses;

public class CategoryResponse
{
    [JsonPropertyName("id")]
    public string CategoryId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("products")]
    public List<ProductResponse> Products { get; set; }
}