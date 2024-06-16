using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PapperCompany.Catalog.Domain;

public class TokenRequest
{
    [JsonIgnore]
    public static string RequestId => Guid.NewGuid().ToString();

    [Required(ErrorMessage = "The user field is required")]
    public string Username { get; set; }

    [Required(ErrorMessage = "The user field is required")]
    public string Role { get; set; }

    [Required(ErrorMessage = "The user field is required")]
    public IEnumerable<string> Claims { get; set; }

}