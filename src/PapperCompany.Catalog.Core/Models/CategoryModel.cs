using PapperCompany.Catalog.Core.Configurations.DTOs;

namespace PapperCompany.Catalog.Core.Models;

public class CategoryModel : CategoryDTO
{
    public List<ProductModel> Products { get; set; }
}
