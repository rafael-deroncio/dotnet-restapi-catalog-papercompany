using System.ComponentModel.DataAnnotations;

namespace PapperCompany.Catalog.Domain.Requests;

public class ProductRequest
{
    [Required(ErrorMessage = "The Name field is required.")]
    [StringLength(100, ErrorMessage = "The Name field must have a maximum of 100 characters.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "The Description field is required.")]
    [StringLength(500, ErrorMessage = "The Description field must have a maximum of 500 characters.")]
    public string Description { get; set; }

    [Required(ErrorMessage = "The Brand field is required.")]
    [StringLength(100, ErrorMessage = "The Brand field must have a maximum of 100 characters.")]
    public string Brand { get; set; }

    [Required(ErrorMessage = "The Model field is required.")]
    [StringLength(100, ErrorMessage = "The Model field must have a maximum of 100 characters.")]
    public string Model { get; set; }

    [Required(ErrorMessage = "The Price field is required.")]
    [Range(0.01, 99999.99, ErrorMessage = "The Price must be between 0.01 and 99999.99.")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "The Stock field is required.")]
    [Range(1, 999, ErrorMessage = "The Stock must be between 1 and 999.")]
    public int Stock { get; set; }
}
