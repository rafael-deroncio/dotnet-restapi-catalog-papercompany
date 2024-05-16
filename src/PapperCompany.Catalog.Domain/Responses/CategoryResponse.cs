using System.Text.Json.Serialization;

namespace PapperCompany.Catalog.Domain.Responses;

public class CategoryResponse
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }
}