using PapperCompany.Catalog.Core.Arguments;
using PapperCompany.Catalog.Core.Configurations.Mapper.Interfaces;
using PapperCompany.Catalog.Core.Exceptions;
using PapperCompany.Catalog.Core.Models;
using PapperCompany.Catalog.Core.Repositories.Interfaces;
using PapperCompany.Catalog.Core.Services.Interfaces;
using PapperCompany.Catalog.Domain.Requests;
using PapperCompany.Catalog.Domain.Responses;
using Serilog;

namespace PapperCompany.Catalog.Core.Services;

public class CategoryService(
    ILogger<CategoryService> logger,
    IObjectConverter mapper,
    IPaginationService paginationService,
    ICategoryRepository categoryRepository) : ICategoryService
{
    private readonly ILogger<CategoryService> _logger = logger;
    private readonly IObjectConverter _mapper = mapper;
    private readonly IPaginationService _paginationService = paginationService;
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    public async Task<PaginationResponse<IEnumerable<CategoryResponse>>> GetCategories(PaginationRequest request)
    {
        _logger.LogInformation("Starting Search for categories with pagination. Request: {0}", request);

        try
        {
            PaginationArgument argument = _mapper.Map<PaginationArgument>(request);

            IEnumerable<CategoryModel> categories = await _categoryRepository.GetCategories(argument);

            return await _paginationService.GetPagination(
                request: _mapper.Map<PaginationRequest>(argument), 
                total: await _categoryRepository.GetTotalRecords(),
                content: _mapper.Map<IEnumerable<CategoryResponse>>(categories));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            throw new CategoryException(
                title: "Categories Paged Error",
                message: "The category search could not be processed at this time."
            );
        }
        finally
        {
            _logger.LogInformation("Finishing search for categories with pagination.");
        }
    }

    public async Task<CategoryResponse> GetCategory(int id)
    {
        var teste = await _categoryRepository.GetCategory(id);
        throw new NotImplementedException();
    }

    public Task<CategoryResponse> CreateCategory(CategoryRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteCategory(int id)
    {
        throw new NotImplementedException();
    }



    public Task<CategoryResponse> UpdateCategory(int id, CategoryRequest request)
    {
        throw new NotImplementedException();
    }
}
