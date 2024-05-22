using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace PapperCompany.Catalog.Domain.Requests;

public class CategoryRequest
{
    [Required(ErrorMessage = "The Name field is required.")]
    [StringLength(100, ErrorMessage = "The Name field must have a maximum of 100 characters.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "The Description field is required.")]
    [StringLength(500, ErrorMessage = "The Description field must have a maximum of 500 characters.")]
    public string Description { get; set; }
}
