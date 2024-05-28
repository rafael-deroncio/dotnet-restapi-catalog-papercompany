using Microsoft.Extensions.Logging;
using NSubstitute;
using PapperCompany.Catalog.Core.Configurations.Mapper.Interfaces;
using PapperCompany.Catalog.Core.Repositories.Interfaces;
using PapperCompany.Catalog.Core.Services;
using PapperCompany.Catalog.Core.Services.Interfaces;

namespace PapperCompany.Catalog.Test.Fistures;

public class ProductServiceFixture
{
    private readonly ILogger<ProductService> _logger;
    private readonly IObjectConverter _mapper;
    private readonly ICategoryService _categoryService;
    private readonly IPaginationService _paginationService;
    private readonly IProductRepository _productRepository;

    public ProductServiceFixture()
    {
        _logger = Substitute.For<ILogger<ProductService>>();
        _mapper = Substitute.For<IObjectConverter>();
        _categoryService = Substitute.For<ICategoryService>();
        _paginationService = Substitute.For<IPaginationService>();
        _productRepository = Substitute.For<IProductRepository>();
    }

    public ProductService InstantiateService()
    {
        return new ProductService(
            _logger,
            _mapper,
            _categoryService,
            _paginationService,
            _productRepository
            );
    }

    
}
