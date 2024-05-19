using PapperCompany.Catalog.Core.Configurations.Mapper.Interfaces;
using PapperCompany.Catalog.Core.Repositories.Interfaces;
using PapperCompany.Catalog.Core.Services.Interfaces;
using PapperCompany.Catalog.Domain.Requests;
using PapperCompany.Catalog.Domain.Responses;

namespace PapperCompany.Catalog.Core.Services;

public class CategoryService(
    ILogger<CategoryService> legger,
    IObjectConverter mapper,
    ICategoryRepository categoryRepository) : ICategoryService
{
    private readonly ILogger<CategoryService> _legger = legger;
    private readonly IObjectConverter _mapper = mapper;
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

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
