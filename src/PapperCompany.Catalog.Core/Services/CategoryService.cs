using PapperCompany.Catalog.Core.Services.Interfaces;
using PapperCompany.Catalog.Domain.Requests;
using PapperCompany.Catalog.Domain.Responses;

namespace PapperCompany.Catalog.Core.Services;

public class CategoryService : ICategoryService
{
    public CategoryService()
    {
        
    }
    
    public Task<CategoryResponse> CreateCategory(CategoryRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteCategory(int id)
    {
        throw new NotImplementedException();
    }

    public Task<PaginationResponse<IEnumerable<CategoryResponse>>> GetCategories(PaginationRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<CategoryResponse> GetCategory(int id)
    {
        throw new NotImplementedException();
    }

    public Task<CategoryResponse> UpdateCategory(int id, CategoryRequest request)
    {
        throw new NotImplementedException();
    }
}
