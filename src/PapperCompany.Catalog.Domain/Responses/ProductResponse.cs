using System.Text.Json;
using System.Text.Json.Serialization;

namespace PapperCompany.Catalog.Domain.Responses;

public class ProductResponse
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("brand")]
    public string Brand { get; set; }

    [JsonPropertyName("model")]
    public string Model { get; set; }

    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    [JsonPropertyName("stock")]
    public int Stock { get; set; }

    [JsonPropertyName("category")]
    public CategoryResponse Category { get; set; }
}
