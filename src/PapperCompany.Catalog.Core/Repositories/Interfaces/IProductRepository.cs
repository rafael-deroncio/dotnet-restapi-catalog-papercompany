using PapperCompany.Catalog.Core.Arguments;
using PapperCompany.Catalog.Core.Models;

namespace PapperCompany.Catalog.Core.Repositories.Interfaces;

public interface IProductRepository
{
    Task<ProductModel> GetProduct(int id);
    Task<IEnumerable<ProductModel>> GetProducts(PaginationArgument argument);
    Task<ProductModel> CreateProduct(ProductArgument argument);
    Task<ProductModel> UpdateProduct(ProductArgument argument);
    Task<ProductModel> DeleteProduct(int id);
}
