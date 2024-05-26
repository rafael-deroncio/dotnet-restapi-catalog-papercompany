using PapperCompany.Catalog.Core.Services;
using PapperCompany.Catalog.Domain.Requests;
using PapperCompany.Catalog.Domain.Responses;
using PapperCompany.Catalog.Test.Fistures;

namespace PapperCompany.Catalog.Test;

public class CategoryServiceTest
{
    [Fact]
    public async Task MustGetCategorySuccessfully()
    {
        // Arrange
        int categoryId = 1;
        CategoryService fixture = new CategoryServiceFixture()
                                      .WithGetCategoryModel()
                                      .WithMapModelToArgument()
                                      .InstantiateService();

        // Act
        CategoryResponse response = await fixture.GetCategory(categoryId);

        // Assert
        Assert.NotNull(response);
    }

    [Fact]
    public async Task MustGetCategoriesSuccessfully()
    {
        // Arrange
        PaginationRequest request = new() { Page = 2, Size = 5 };
        CategoryService fixture = new CategoryServiceFixture()
                                      .WithMapRequestToArgument()
                                      .WithCategoryModelList()
                                      .WithMapArgumentToRequest()
                                      .WithPaginationTotalRecords()
                                      .WithMapModelToResponseList()
                                      .WithPaginationResponse()
                                      .InstantiateService();

        // Act
        PaginationResponse<IEnumerable<CategoryResponse>> 
            response = await fixture.GetCategories(request);

        // Assert
        Assert.NotNull(response.Data);
    }

    [Fact]
    public async Task MustCreateNewCategorySuccessfully()
    {
        // Arrange
        CategoryServiceFixture fixture = new();
        CategoryRequest request = fixture.CategoryRequestMock();
        CategoryService service = fixture.WithCreateCategoryModel()
                                         .WithMapModelToResponse()
                                         .InstantiateService();

        // Act
        CategoryResponse response = await service.CreateCategory(request);

        // Assert
        Assert.NotNull(response);
    }

    [Fact]
    public async Task MustEditCategorySuccessfully()
    {
        // Arrange
        CategoryServiceFixture fixture = new();
        int id = 1;
        CategoryRequest request = fixture.CategoryRequestMock();
        CategoryService service = fixture.WithUpdateCategoryModel()
                                         .WithGetCategoryModel()
                                         .WithMapModelToResponse()
                                         .InstantiateService();

        // Act
        CategoryResponse response = await service.UpdateCategory(id, request);

        // Assert
        Assert.NotNull(response);
    }

    [Fact]
    public async Task MustDeleteCategoryIsTrue()
    {
        // Arrange
        int id = 1;
        bool result = true;
        CategoryService service = new CategoryServiceFixture()
                                         .WithDeleteCategoryModel(result)
                                         .WithGetCategoryModel()
                                         .InstantiateService();

        // Act
        bool response = await service.DeleteCategory(id);

        // Assert
        Assert.True(response);
    }

    [Fact]
    public async Task MustDeleteCategoryIsFalse()
    {
        // Arrange
        int id = 1;
        bool result = false;
        CategoryService service = new CategoryServiceFixture()
                                         .WithDeleteCategoryModel(result)
                                         .WithGetCategoryModel()
                                         .InstantiateService();

        // Act
        bool response = await service.DeleteCategory(id);

        // Assert
        Assert.False(response);
    }
}
