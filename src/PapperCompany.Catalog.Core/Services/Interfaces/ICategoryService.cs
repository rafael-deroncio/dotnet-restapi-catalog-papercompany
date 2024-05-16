using PapperCompany.Catalog.Domain.Requests;
using PapperCompany.Catalog.Domain.Responses;

namespace PapperCompany.Catalog.Core.Services.Interfaces;

public interface ICategoryService
{
    Task<CategoryResponse> GetCategory(int id);
    Task<PaginationResponse<IEnumerable<CategoryResponse>>> GetCategories(PaginationRequest request);
    Task<CategoryResponse> CreateCategory(CategoryRequest request);
    Task<CategoryResponse> UpdateCategory(int id, CategoryRequest request);
    Task<bool> DeleteCategory(int id);
}
