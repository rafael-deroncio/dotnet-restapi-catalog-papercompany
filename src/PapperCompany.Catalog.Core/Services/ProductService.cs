using PapperCompany.Catalog.Core.Services.Interfaces;
using PapperCompany.Catalog.Domain.Requests;
using PapperCompany.Catalog.Domain.Responses;

namespace PapperCompany.Catalog.Core.Services;

public class ProductService : IProductService
{
    public ProductService()
    {
        
    }
    
    public Task<ProductResponse> CreateProduct(ProductRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteProduct(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ProductResponse> GetProduct(int id)
    {
        throw new NotImplementedException();
    }

    public Task<PaginationResponse<IEnumerable<ProductResponse>>> GetProducts(PaginationRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<ProductResponse> UpdateProduct(int id, ProductRequest request)
    {
        throw new NotImplementedException();
    }
}
