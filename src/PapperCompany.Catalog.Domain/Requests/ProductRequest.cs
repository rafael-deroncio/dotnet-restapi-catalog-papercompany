using System.ComponentModel.DataAnnotations;

namespace PapperCompany.Catalog.Domain.Requests;

/// <summary>
/// Represents a request to create or update a product in the catalog.
/// </summary>
public class ProductRequest
{
    /// <summary>
    /// Gets or sets the name of the product.
    /// This field is required and must have a maximum length of 100 characters.
    /// </summary>
    [Required(ErrorMessage = "The Name field is required.")]
    [StringLength(100, ErrorMessage = "The Name field must have a maximum of 100 characters.")]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the description of the product.
    /// This field is required and must have a maximum length of 500 characters.
    /// </summary>
    [Required(ErrorMessage = "The Description field is required.")]
    [StringLength(500, ErrorMessage = "The Description field must have a maximum of 500 characters.")]
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the brand of the product.
    /// This field is required and must have a maximum length of 100 characters.
    /// </summary>
    [Required(ErrorMessage = "The Brand field is required.")]
    [StringLength(100, ErrorMessage = "The Brand field must have a maximum of 100 characters.")]
    public string Brand { get; set; }

    /// <summary>
    /// Gets or sets the model of the product.
    /// This field is required and must have a maximum length of 100 characters.
    /// </summary>
    [Required(ErrorMessage = "The Model field is required.")]
    [StringLength(100, ErrorMessage = "The Model field must have a maximum of 100 characters.")]
    public string Model { get; set; }

    /// <summary>
    /// Gets or sets the price of the product.
    /// This field is required and must be between 0.01 and 99999.99.
    /// </summary>
    [Required(ErrorMessage = "The Price field is required.")]
    [Range(0.01, 99999.99, ErrorMessage = "The Price must be between 0.01 and 99999.99.")]
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the stock quantity of the product.
    /// This field is required and must be between 1 and 999.
    /// </summary>
    [Required(ErrorMessage = "The Stock field is required.")]
    [Range(1, 999, ErrorMessage = "The Stock must be between 1 and 999.")]
    public int Stock { get; set; }

    /// <summary>
    /// Gets or sets the category ID of the product.
    /// This field is required.
    /// </summary>
    [Required(ErrorMessage = "The Category field is required.")]
    public int CategoryId { get; set; }
}
