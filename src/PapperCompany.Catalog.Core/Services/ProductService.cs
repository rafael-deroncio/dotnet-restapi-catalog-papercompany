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

public class ProductService(
    ILogger<ProductService> logger,
    IObjectConverter mapper,
    ICategoryService categoryService,
    IPaginationService paginationService,
    IProductRepository productRepository) : IProductService
{
    private readonly ILogger<ProductService> _logger = logger;
    private readonly IObjectConverter _mapper = mapper;
    private readonly ICategoryService _categoryService = categoryService;
    private readonly IPaginationService _paginationService = paginationService;
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<PaginationResponse<IEnumerable<ProductResponse>>> GetProducts(PaginationRequest request)
    {
        _logger.LogInformation("Starting search for products with pagination. Request: {0}.", JsonSerializer.Serialize(request));

        try
        {
            PaginationArgument argument = _mapper.Map<PaginationArgument>(request);

            IEnumerable<ProductModel> products = await _productRepository.GetProducts(argument);

            return await _paginationService.GetPagination(
                request: _mapper.Map<PaginationRequest>(argument),
                total: await _productRepository.GetTotalRecords(),
                content: _mapper.Map<IEnumerable<ProductResponse>>(products));
        }
        catch (BaseException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            throw new ProductException(
                title: "Products paged Error",
                message: "The product search could not be processed at this time."
            );
        }
        finally
        {
            _logger.LogInformation("Finishing search for products with pagination.");
        }
    }

    public async Task<ProductResponse> GetProduct(int id)
    {
        _logger.LogInformation("Starting search for product by ID: {0}.", id);

        try
        {
            ProductModel product = await _productRepository.GetProduct(id)
                ?? throw new ProductException(
                    title: "Product not found",
                    message: string.Format("Product with ID {0} not found", id),
                    code: HttpStatusCode.NotFound
                );

            return _mapper.Map<ProductResponse>(product);
        }
        catch (BaseException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            throw new ProductException(
                title: "Product get by id error",
                message: "The product get could not be processed at this time."
            );
        }
        finally
        {
            _logger.LogInformation("Finishing search for product by ID.");
        }
    }

    public async Task<ProductResponse> CreateProduct(ProductRequest request)
    {
        _logger.LogInformation("Starting create for product with: {0}", JsonSerializer.Serialize(request));

        try
        {
            ValidateSQLInjection(request);

            if (await _categoryService.GetCategory(request.CategoryId) == null)
                throw new ProductException(
                    title: "Product create error",
                    message: string.Format("Product category with ID {0} not found!", request.CategoryId),
                    code: HttpStatusCode.BadRequest);

            ProductArgument argument = _mapper.Map<ProductArgument>(request);
            argument.Name = request.Name.ToCamelCase();
            argument.Description = request.Description.Trim();
            argument.Category = new() { CategoryId = request.CategoryId };
            argument.Active = true;
            argument.CreatedAt = DateTime.Now;
            argument.UpdatedAt = DateTime.Now;

            ProductModel model = await _productRepository.CreateProduct(argument);

            return _mapper.Map<ProductResponse>(model);
        }
        catch (BaseException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            throw new ProductException(
                title: "Product create error",
                message: "The product create could not be processed at this time."
            );
        }
        finally
        {
            _logger.LogInformation("Finishing create for product.");
        }
    }

    public async Task<ProductResponse> UpdateProduct(int id, ProductRequest request)
    {
        _logger.LogInformation("Starting update for product with: {0}", JsonSerializer.Serialize(request));

        try
        {
            ValidateSQLInjection(request);

            ProductModel product = await _productRepository.GetProduct(id) ?? throw new ProductException(
                    title: "Product update error",
                    message: string.Format("Product with ID {0} not found!", id),
                    code: HttpStatusCode.BadRequest);

            if (await _categoryService.GetCategory(request.CategoryId) == null)
                throw new ProductException(
                    title: "Product update error",
                    message: string.Format("Product category with ID {0} not found!", request.CategoryId),
                    code: HttpStatusCode.BadRequest);

            ProductArgument argument = _mapper.Map<ProductArgument>(request);
            argument.Name = request.Name.ToCamelCase();
            argument.Description = request.Description.Trim();
            argument.Category = new() { CategoryId = request.CategoryId };
            argument.UpdatedAt = DateTime.Now;

            ProductModel model = await _productRepository.UpdateProduct(argument);

            return _mapper.Map<ProductResponse>(model);
        }
        catch (BaseException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            throw new ProductException(
                title: "Product update error",
                message: "The product update could not be processed at this time."
            );
        }
        finally
        {
            _logger.LogInformation("Finishing update for product");
        }
    }

    public async Task<bool> DeleteProduct(int id)
    {
        _logger.LogInformation("Starting delete for product with id: {0}", id);

        try
        {
            if (await _productRepository.GetProduct(id) == null)
                throw new ProductException(
                    title: "Product update error",
                    message: string.Format("Product with ID {0} not found!", id),
                    code: HttpStatusCode.BadRequest);

            return await _productRepository.DeleteProduct(id);
        }
        catch (BaseException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            throw new ProductException(
                title: "Product delete error",
                message: "The product delete could not be processed at this time."
            );
        }
        finally
        {
            _logger.LogInformation("Finishing delete for product");
        }
    }

    private static void ValidateSQLInjection(ProductRequest request)
    {
        if (request.Name.ContainsSqlInjection() ||
            request.Description.ContainsSqlInjection() ||
            request.Model.ContainsSqlInjection() ||
            request.Brand.ContainsSqlInjection())
            throw new ProductException(
                title: "Product create error",
                message: "Invalid data!",
                code: HttpStatusCode.BadRequest
            );
    }

}
