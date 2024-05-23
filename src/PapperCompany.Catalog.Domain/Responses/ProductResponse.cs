using System.Text.Json.Serialization;

namespace PapperCompany.Catalog.Domain.Responses;

/// <summary>
/// Represents the response containing product details.
/// </summary>
public class ProductResponse
{
    /// <summary>
    /// Gets or sets the name of the product.
    /// This property maps to the JSON property 'name'.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the description of the product.
    /// This property maps to the JSON property 'description'.
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the brand of the product.
    /// This property maps to the JSON property 'brand'.
    /// </summary>
    [JsonPropertyName("brand")]
    public string Brand { get; set; }

    /// <summary>
    /// Gets or sets the model of the product.
    /// This property maps to the JSON property 'model'.
    /// </summary>
    [JsonPropertyName("model")]
    public string Model { get; set; }

    /// <summary>
    /// Gets or sets the price of the product.
    /// This property maps to the JSON property 'price'.
    /// </summary>
    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the stock quantity of the product.
    /// This property maps to the JSON property 'stock'.
    /// </summary>
    [JsonPropertyName("stock")]
    public int Stock { get; set; }

    /// <summary>
    /// Gets or sets the category details of the product.
    /// This property maps to the JSON property 'category'.
    /// </summary>
    [JsonPropertyName("category")]
    public CategoryResponse Category { get; set; }
}