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

    public CategoryServiceFixture WithGetCategoryModel()
    {
        CategoryModel model = _fixture.Create<CategoryModel>();
        int id = Arg.Any<int>();
        _categoryRepository.GetCategory(id).ReturnsForAnyArgs(model);
        return this;
    }

    public CategoryServiceFixture WithCreateCategoryModel()
    {
        CategoryModel model = _fixture.Create<CategoryModel>();
        CategoryArgument argument = Arg.Any<CategoryArgument>();
        _categoryRepository.CreateCategory(argument).ReturnsForAnyArgs(model);
        return this;
    }

    public CategoryServiceFixture WithUpdateCategoryModel()
    {
        CategoryModel model = _fixture.Create<CategoryModel>();
        CategoryArgument argument = Arg.Any<CategoryArgument>();
        _categoryRepository.UpdateCategory(argument).ReturnsForAnyArgs(model);
        return this;
    }

    public CategoryServiceFixture WithDeleteCategoryModel(bool ?result)
    {
        result = result == null ? _fixture.Create<bool>() : result;
        int id = Arg.Any<int>();
        _categoryRepository.DeleteCategory(id).ReturnsForAnyArgs(result.Value);
        return this;
    }

    public CategoryServiceFixture WithCategoryModelList()
    {
        IEnumerable<CategoryModel> models = _fixture.CreateMany<CategoryModel>();
        PaginationArgument argument = Arg.Any<PaginationArgument>();
        _categoryRepository.GetCategories(argument).ReturnsForAnyArgs(models);
        return this;
    }

    public CategoryServiceFixture WithPaginationResponse()
    {
        PaginationResponse<IEnumerable<CategoryResponse>> response = _fixture.Create<PaginationResponse<IEnumerable<CategoryResponse>>>();
        PaginationRequest request = Arg.Any<PaginationRequest>();
        int total = Arg.Any<int>();
        IEnumerable<CategoryResponse> content = Arg.Any<IEnumerable<CategoryResponse>>();
        _paginationService.GetPagination(request, total, content).Returns(response);
        return this;
    }

    public CategoryServiceFixture WithPaginationTotalRecords(int records = 0)
    {
        int total = records == 0 ? _fixture.Create<int>() : records;
        _categoryRepository.GetTotalRecords().Returns(total);
        return this;
    }

    #region Mappers
    public CategoryServiceFixture WithMapModelToArgument()
    {
        CategoryResponse response = _fixture.Create<CategoryResponse>();
        CategoryModel model = Arg.Any<CategoryModel>();
        _mapper.Map<CategoryResponse>(model).Returns(response);
        return this;
    }

    public CategoryServiceFixture WithMapRequestToArgument()
    {
        PaginationArgument argument = _fixture.Create<PaginationArgument>();
        PaginationRequest request = Arg.Any<PaginationRequest>();
        _mapper.Map<PaginationArgument>(request).Returns(argument);
        return this;
    }

    public CategoryServiceFixture WithMapArgumentToRequest()
    {
        var request = _fixture.Create<PaginationRequest>();
        var argument = Arg.Any<PaginationArgument>();
        _mapper.Map<PaginationRequest>(argument).Returns(request);
        return this;
    }

    public CategoryServiceFixture WithMapModelToResponse()
    {
        CategoryModel model = Arg.Any<CategoryModel>(); 
        CategoryResponse reponse = _fixture.Create<CategoryResponse>();
        _mapper.Map<CategoryResponse>(model).Returns(reponse);
        return this;
    }

    public CategoryServiceFixture WithMapModelToResponseList()
    {
        IEnumerable<CategoryModel> models = Arg.Any<IEnumerable<CategoryModel>>(); 
        IEnumerable<CategoryResponse> reponses = _fixture.CreateMany<CategoryResponse>();
        _mapper.Map<IEnumerable<CategoryResponse>>(models).Returns(reponses);
        return this;
    }

    #endregion

    #region Mocks
    public CategoryRequest CategoryRequestMock()
    {
        return _fixture.Create<CategoryRequest>();
    } 
    #endregion
}
