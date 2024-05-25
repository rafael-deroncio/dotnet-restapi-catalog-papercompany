using System.Net;
using System.Text.Json;
using PapperCompany.Catalog.Core.Arguments;
using PapperCompany.Catalog.Core.Configurations.Mapper.Interfaces;
using PapperCompany.Catalog.Core.Exceptions;
using PapperCompany.Catalog.Core.Models;
using PapperCompany.Catalog.Core.Repositories.Interfaces;
using PapperCompany.Catalog.Core.Services.Interfaces;
using PapperCompany.Catalog.Domain.Requests;
using PapperCompany.Catalog.Domain.Responses;

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
        _logger.LogInformation("Starting Search for categories with pagination. Request: {0}.", JsonSerializer.Serialize(request));

        try
        {
            PaginationArgument argument = _mapper.Map<PaginationArgument>(request);

            IEnumerable<CategoryModel> categories = await _categoryRepository.GetCategories(argument);

            var pRequest = _mapper.Map<PaginationRequest>(argument);
            var pTotal = await _categoryRepository.GetTotalRecords();
            var pContent = _mapper.Map<IEnumerable<CategoryResponse>>(categories);
            var log = pContent.ToList();
            return await _paginationService.GetPagination(
                request: pRequest,
                total: pTotal,
                content: pContent
                );
        }
        catch (BaseException)
        {
            throw;
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
        _logger.LogInformation("Starting Search for category by ID: {0}.", id);

        try
        {
            CategoryModel category = await _categoryRepository.GetCategory(id)
                ?? throw new CategoryException(
                    title: "Category not found",
                    message: string.Format("Category with ID {0} not found", id),
                    code: HttpStatusCode.NotFound
                );

            return _mapper.Map<CategoryResponse>(category);
        }
        catch (BaseException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            throw new CategoryException(
                title: "Category get by id error",
                message: "The category get could not be processed at this time."
            );
        }
        finally
        {
            _logger.LogInformation("Finishing search for category by ID.");
        }
    }

    public async Task<CategoryResponse> CreateCategory(CategoryRequest request)
    {
        _logger.LogInformation("Starting Create for Category with: {0}", JsonSerializer.Serialize(request));

        try
        {
            if (request.Name.ContainsSqlInjection() || request.Description.ContainsSqlInjection())
                throw new CategoryException(
                    title: "Category create error",
                    message: "Invalid data!",
                    code: HttpStatusCode.BadRequest
                );

            CategoryArgument argument = new()
            {
                Name = request.Name.ToCamelCase(),
                Description = request.Description.Trim(),
                Active = true,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            CategoryModel model = await _categoryRepository.CreateCategory(argument);

            return _mapper.Map<CategoryResponse>(model);
        }
        catch (BaseException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            throw new CategoryException(
                title: "Category create error",
                message: "The category create could not be processed at this time."
            );
        }
        finally
        {
            _logger.LogInformation("Finishing search for category.");
        }
    }

    public async Task<CategoryResponse> UpdateCategory(int id, CategoryRequest request)
    {
        _logger.LogInformation("Starting update category with ID: {0}, request: {1}", id, JsonSerializer.Serialize(request));

        try
        {
            if (request.Name.ContainsSqlInjection() || request.Description.ContainsSqlInjection())
                throw new CategoryException(
                    title: "Category update error",
                    message: "Invalid data!",
                    code: HttpStatusCode.BadRequest
                );

            if (await _categoryRepository.GetCategory(id) == null)
                throw new CategoryException(
                    title: "Category update error",
                    message: string.Format("Category with ID {0} not found", id),
                    code: HttpStatusCode.NotFound
                );

            CategoryArgument argument = new()
            {
                CategoryId = id,
                Name = request.Name.ToCamelCase(),
                Description = request.Description.Trim(),
                UpdatedAt = DateTime.Now
            };

            CategoryModel model = await _categoryRepository.UpdateCategory(argument);

            return _mapper.Map<CategoryResponse>(model);
        }
        catch (BaseException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            throw new CategoryException(
                title: "Category update error",
                message: "The category update could not be processed at this time."
            );
        }
        finally
        {
            _logger.LogInformation("Finishing update category.");
        }
    }

    public async Task<bool> DeleteCategory(int id)
    {
        _logger.LogInformation("Starting delete category with ID: {0}", id);

        try
        {
            if (await _categoryRepository.GetCategory(id) == null)
                throw new CategoryException(
                    title: "Category delete error",
                    message: string.Format("Category with ID {0} not found", id),
                    code: HttpStatusCode.NotFound
                );

            return await _categoryRepository.DeleteCategory(id);
        }
        catch (BaseException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            throw new CategoryException(
                title: "Category delete error",
                message: "The category delete could not be processed at this time."
            );
        }
        finally
        {
            _logger.LogInformation("Finishing delete category");
        }
    }
}
