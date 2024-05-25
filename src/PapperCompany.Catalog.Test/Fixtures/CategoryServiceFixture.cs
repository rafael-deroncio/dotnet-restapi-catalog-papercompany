using Microsoft.Extensions.Logging;
using NSubstitute;
using PapperCompany.Catalog.Core.Configurations.Mapper.Interfaces;
using PapperCompany.Catalog.Core.Repositories.Interfaces;
using PapperCompany.Catalog.Core.Services;
using PapperCompany.Catalog.Core.Services.Interfaces;

namespace PapperCompany.Catalog.Test.Fistures;

public class CategoryServiceFixture
{
    private readonly ILogger<CategoryService> _logger;
    private readonly IObjectConverter _mapper;
    private readonly IPaginationService _paginationService;
    private readonly ICategoryRepository _categoryRepository;

    public CategoryServiceFixture()
    {
        _logger = Substitute.For<ILogger<CategoryService>>();
        _mapper = Substitute.For<IObjectConverter>();
        _paginationService = Substitute.For<IPaginationService>();
        _categoryRepository = Substitute.For<ICategoryRepository>();
    }

    public CategoryService InstantiateService()
    {
        return new CategoryService(
            _logger,
            _mapper,
            _paginationService,
            _categoryRepository
            );
    }

    
}
