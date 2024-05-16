using PapperCompany.Catalog.Domain.Requests;
using PapperCompany.Catalog.Domain.Responses;

namespace PapperCompany.Catalog.Core.Services.Interfaces;

public interface IProductService
{
    Task<ProductResponse> GetProduct(int id);
    Task<PaginationResponse<IEnumerable<ProductResponse>>> GetProducts(PaginationRequest request);
    Task<ProductResponse> CreateProduct(ProductRequest request);
    Task<ProductResponse> UpdateProduct(int id, ProductRequest request);
    Task<bool> DeleteProduct(int id);
}
