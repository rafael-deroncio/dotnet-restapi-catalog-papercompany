using PapperCompany.Catalog.Core.Arguments;
using PapperCompany.Catalog.Core.Models;
using PapperCompany.Catalog.Core.Repositories.Interfaces;

namespace PapperCompany.Catalog.Core.Repositories;

public class CategoryRepository(
    IConfiguration configuration,
    ILogger<CategoryRepository> logger) : BaseRepository(configuration), ICategoryRepository
{

    private readonly ILogger<CategoryRepository> _logger = logger;

    public Task<IEnumerable<CategoryModel>> GetCategories(PaginationArgument argument)
    {
        throw new NotImplementedException();
    }

    public Task<CategoryModel> GetCategory(int id)
    {
        throw new NotImplementedException();
    }

    public Task<CategoryModel> CreateCategory(CategoryArgument argument)
    {
        throw new NotImplementedException();
    }

    public Task<CategoryModel> UpdateCategory(CategoryArgument argument)
    {
        throw new NotImplementedException();
    }

    public Task<CategoryModel> DeleteCategory(int id)
    {
        throw new NotImplementedException();
    }
}
