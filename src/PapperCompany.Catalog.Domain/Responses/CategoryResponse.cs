using System.Text.Json.Serialization;

namespace PapperCompany.Catalog.Domain.Responses;

/// <summary>
/// Represents the response containing category details.
/// </summary>
public class CategoryResponse
{
    /// <summary>
    /// Gets or sets the unique identifier for the category.
    /// This property maps to the JSON property 'id'.
    /// </summary>
    [JsonPropertyName("id")]
    public string CategoryId { get; set; }

    /// <summary>
    /// Gets or sets the name of the category.
    /// This property maps to the JSON property 'name'.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the description of the category.
    /// This property maps to the JSON property 'description'.
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the list of products associated with the category.
    /// This property maps to the JSON property 'products'.
    /// </summary>
    [JsonPropertyName("products")]
    public List<ProductResponse> Products { get; set; }
}