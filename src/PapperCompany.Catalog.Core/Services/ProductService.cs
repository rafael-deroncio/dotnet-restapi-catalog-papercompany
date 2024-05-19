using PapperCompany.Catalog.Core.Configurations.Mapper.Interfaces;
using PapperCompany.Catalog.Core.Repositories.Interfaces;
using PapperCompany.Catalog.Core.Services.Interfaces;
using PapperCompany.Catalog.Domain.Requests;
using PapperCompany.Catalog.Domain.Responses;

namespace PapperCompany.Catalog.Core.Services;

public class ProductService(
    ILogger<ProductService> legger,
    IObjectConverter mapper,
    IProductRepository ProductRepository) : IProductService
{
    private readonly ILogger<ProductService> _legger = legger;
    private readonly IObjectConverter _mapper = mapper;
    private readonly IProductRepository _ProductRepository = ProductRepository;

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
