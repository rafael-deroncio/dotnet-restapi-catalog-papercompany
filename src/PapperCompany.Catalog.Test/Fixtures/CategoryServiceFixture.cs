using AutoFixture;
using Microsoft.Extensions.Logging;
using NSubstitute;
using PapperCompany.Catalog.Core.Arguments;
using PapperCompany.Catalog.Core.Configurations.Mapper;
using PapperCompany.Catalog.Core.Configurations.Mapper.Interfaces;
using PapperCompany.Catalog.Core.Models;
using PapperCompany.Catalog.Core.Repositories.Interfaces;
using PapperCompany.Catalog.Core.Services;
using PapperCompany.Catalog.Core.Services.Interfaces;
using PapperCompany.Catalog.Domain.Requests;
using PapperCompany.Catalog.Domain.Responses;

namespace PapperCompany.Catalog.Test.Fistures;

public class CategoryServiceFixture
{
    private readonly IFixture _fixture;
    private readonly ILogger<CategoryService> _logger;
    private readonly IObjectConverter _mapper;
    private readonly IPaginationService _paginationService;
    private readonly ICategoryRepository _categoryRepository;

    public CategoryServiceFixture()
    {
        _fixture = new Fixture();
        _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _logger = Substitute.For<ILogger<CategoryService>>();
        _mapper = new ObjectConverter();
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

    public CategoryServiceFixture WithCategoryModel()
    {
        CategoryModel model = _fixture.Create<CategoryModel>();
        _categoryRepository.GetCategory(Arg.Any<int>()).ReturnsForAnyArgs(model);
        return this;
    }

    public CategoryServiceFixture WithCategoryModelList()
    {
        IEnumerable<CategoryModel> models = _fixture.CreateMany<CategoryModel>();
        _categoryRepository.GetCategories(Arg.Any<PaginationArgument>()).ReturnsForAnyArgs(models);
        return this;
    }

    public CategoryServiceFixture WithPaginationResponse<T>()
    {
        PaginationResponse<T> response = _fixture.Create<PaginationResponse<T>>();
        _paginationService.GetPagination<T>(
            Arg.Any<PaginationRequest>(), 
            Arg.Any<int>(), 
            Arg.Any<T>()).Returns(response);
        return this;
    }

    public CategoryServiceFixture WithPaginationTotalRecords(int records = 0)
    {
        int total = records == 0 ? _fixture.Create<int>() : records;
        _categoryRepository.GetTotalRecords().Returns(total);
        return this;
    }
}
