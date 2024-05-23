using System.ComponentModel.DataAnnotations;

namespace PapperCompany.Catalog.Domain.Requests;

/// <summary>
/// Represents a request to create or update a category in the catalog.
/// </summary>
public class CategoryRequest
{
    /// <summary>
    /// Gets or sets the name of the category.
    /// This field is required and must have a maximum length of 100 characters.
    /// </summary>
    [Required(ErrorMessage = "The Name field is required.")]
    [StringLength(100, ErrorMessage = "The Name field must have a maximum of 100 characters.")]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the description of the category.
    /// This field is required and must have a maximum length of 500 characters.
    /// </summary>
    [Required(ErrorMessage = "The Description field is required.")]
    [StringLength(500, ErrorMessage = "The Description field must have a maximum of 500 characters.")]
    public string Description { get; set; }
}