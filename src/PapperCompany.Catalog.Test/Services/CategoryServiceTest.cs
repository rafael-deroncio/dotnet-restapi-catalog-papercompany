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
                                      .WithCategoryModel()
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

        // Act

        // Assert
        await Task.FromResult(0);
    }

    [Fact]
    public async Task MustEditCategorySuccessfully()
    {
        // Arrange

        // Act

        // Assert
        await Task.FromResult(0);
    }

    [Fact]
    public async Task MustDeleteCategorySuccessfully()
    {
        // Arrange

        // Act

        // Assert
        await Task.FromResult(0);
    }

    [Fact]
    public async Task MustReturnBadRequest()
    {
        // Arrange

        // Act

        // Assert
        await Task.FromResult(0);
    }

    [Fact]
    public async Task MustReturnNotFound()
    {
        // Arrange

        // Act

        // Assert
        await Task.FromResult(0);
    }
}
