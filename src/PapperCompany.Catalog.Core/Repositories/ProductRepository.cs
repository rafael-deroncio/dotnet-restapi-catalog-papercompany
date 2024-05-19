using PapperCompany.Catalog.Core.Arguments;
using PapperCompany.Catalog.Core.Models;
using PapperCompany.Catalog.Core.Repositories.Interfaces;

namespace PapperCompany.Catalog.Core.Repositories;

public class ProductRepository(IConfiguration configuration) : BaseRepository(configuration), IProductRepository
{
    public Task<IEnumerable<ProductModel>> GetProducts(PaginationArgument argument)
    {
        throw new NotImplementedException();
    }

    public Task<ProductModel> GetProduct(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ProductModel> CreateProduct(ProductArgument argument)
    {
        throw new NotImplementedException();
    }

    public Task<ProductModel> UpdateProduct(ProductArgument argument)
    {
        throw new NotImplementedException();
    }

    public Task<ProductModel> DeleteProduct(int id)
    {
        throw new NotImplementedException();
    }
}
