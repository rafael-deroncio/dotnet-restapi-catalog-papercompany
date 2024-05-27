using AutoFixture;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.Core.Arguments;
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

    public CategoryServiceFixture WithGetCategory(int? id = null, bool success = true)
    {
        id ??= Arg.Any<int>();
        CategoryModel? model = success ? _fixture.Create<CategoryModel>() : null;
        _categoryRepository.GetCategory(id.Value).Returns(model);
        return this;
    }

    public CategoryServiceFixture WithGetCategories()
    {
        PaginationArgument argument = Arg.Any<PaginationArgument>();
        IEnumerable<CategoryModel> models = _fixture.CreateMany<CategoryModel>();
        _categoryRepository.GetCategories(argument).Returns(models);
        return this;
    }

    public CategoryServiceFixture WithGetPagination()
    {
        PaginationRequest request = Arg.Any<PaginationRequest>();
        int total = Arg.Any<int>();
        IEnumerable<CategoryResponse> content = Arg.Any<IEnumerable<CategoryResponse>>();
        PaginationResponse<CategoryResponse> response = _fixture.Create<PaginationResponse<CategoryResponse>>();
        _paginationService.GetPagination(request, total, content).Returns(response);
        return this;
    }

    public CategoryServiceFixture WithGetTotalRecords()
    {
        int total = _fixture.Create<int>();
        _categoryRepository.GetTotalRecords().Returns(total);
        return this;
    }

    public CategoryServiceFixture WithCreateCategory()
    {
        CategoryArgument argument = Arg.Any<CategoryArgument>();
        CategoryModel model = _fixture.Create<CategoryModel>();
        _categoryRepository.CreateCategory(argument).Returns(model);
        return this;
    }

    public CategoryServiceFixture WithUpdateCategory()
    {
        CategoryArgument argument = Arg.Any<CategoryArgument>();
        CategoryModel model = _fixture.Create<CategoryModel>();
        _categoryRepository.UpdateCategory(argument).Returns(model);
        return this;
    }

    public CategoryServiceFixture WithDeleteCategory(bool result = true)
    {
        int id = Arg.Any<int>();
        _categoryRepository.DeleteCategory(id).Returns(result);
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
    public CategoryRequest CategoryRequestMock(string name = "", string description = "")
    {
        var category = _fixture.Create<CategoryRequest>();
        category.Name = string.IsNullOrEmpty(name) ? category.Name : name;
        category.Description = string.IsNullOrEmpty(description) ? category.Description : description;
        return category;
    }

    public PaginationRequest PaginationRequestMock()
    {
        return _fixture.Create<PaginationRequest>();
    }
    #endregion
}
