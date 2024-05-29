using System.Net;
using PapperCompany.Catalog.Core.Exceptions;
using PapperCompany.Catalog.Core.Services;
using PapperCompany.Catalog.Domain.Requests;
using PapperCompany.Catalog.Domain.Responses;
using PapperCompany.Catalog.Test.Fistures;

namespace PapperCompany.Catalog.Test.Services;

public class CategoryServiceTest
{
    [Fact]
    public async Task MustGetCategorySuccessfully()
    {
        // Arrange
        int id = 1;
        bool success = true;
        CategoryService fixture = new CategoryServiceFixture()
                                      .WithGetCategory(id, success)
                                      .WithMapModelToResponse()
                                      .InstantiateService();

        // Act
        CategoryResponse response = await fixture.GetCategory(id);

        // Assert
        Assert.NotNull(response);
    }

    [Fact]
    public async Task MustGetCategoryAndReturnNotFound()
    {
        // Arrange
        int id = 1;
        bool success = false;
        string message = string.Format("Category with ID {0} not found", id);
        CategoryService fixture = new CategoryServiceFixture()
                                      .WithGetCategory(id, success)
                                      .WithMapModelToResponse()
                                      .InstantiateService();

        // Act
        CategoryException exception = await Assert.ThrowsAsync<CategoryException>(async () =>
        {
            await fixture.GetCategory(id);
        });

        // Assert
        Assert.NotNull(exception);
        Assert.Equal(message, exception.Message);
        Assert.Equal(HttpStatusCode.NotFound, exception.Code);
    }

    [Fact]
    public async Task MustGetCategoriesSuccessfully()
    {
        // Arrange
        CategoryServiceFixture fixture = new CategoryServiceFixture();
        PaginationRequest request = fixture.PaginationRequestMock();
        CategoryService service = fixture.WithGetCategories()
                                         .WithGetPagination()
                                         .WithGetTotalRecords()
                                         .WithMapRequestToArgument()
                                         .WithMapArgumentToRequest()
                                         .WithMapModelToResponseList()
                                         .InstantiateService();

        // Act
        PaginationResponse<CategoryResponse> response = await service.GetCategories(request);

        // Assert
        Assert.True(response.PageNumber > 0);
        Assert.True(response.PageSize > 0);
        Assert.NotNull(response.Data);
    }

    [Fact]
    public async Task MustCreateNewCategorySuccessfully()
    {
        // Arrange
        CategoryServiceFixture fixture = new();
        CategoryRequest request = fixture.CategoryRequestMock();
        CategoryService service = fixture.WithCreateCategory()
                                         .WithMapModelToResponse()
                                         .InstantiateService();

        // Act
        CategoryResponse response = await service.CreateCategory(request);

        // Assert
        Assert.NotNull(response);
    }

    [Fact]
    public async Task MustCreateNewCategoryAndReturnBadRequest()
    {
        // Arrange
        HttpStatusCode codeExpected = HttpStatusCode.BadRequest;
        string titleExpected = "Category create error";
        string messageExpected = "Invalid data!";
        string nameSqlInjection = "SELECT * FROM users; --";
        CategoryServiceFixture fixture = new();
        CategoryRequest request = fixture.CategoryRequestMock(nameSqlInjection);
        CategoryService service = fixture.WithCreateCategory()
                                         .WithMapModelToResponse()
                                         .InstantiateService();

        // Act
        CategoryException exception = await Assert.ThrowsAsync<CategoryException>(
            () => service.CreateCategory(request)
        );

        // Assert
        Assert.Equal(codeExpected, exception.Code);
        Assert.Equal(titleExpected, exception.Title);
        Assert.Equal(messageExpected, exception.Message);
    }

    [Fact]
    public async Task MustEditCategorySuccessfully()
    {
        // Arrange
        CategoryServiceFixture fixture = new();
        int id = 1;
        bool success = true;
        CategoryRequest request = fixture.CategoryRequestMock();
        CategoryService service = fixture.WithGetCategory(id, success)
                                         .WithUpdateCategory()
                                         .WithMapModelToResponse()
                                         .InstantiateService();

        // Act
        CategoryResponse response = await service.UpdateCategory(id, request);

        // Assert
        Assert.NotNull(response);
    }

    [Fact]
    public async Task MustEditCategoryAndReturnNotFound()
    {
        // Arrange
        CategoryServiceFixture fixture = new();
        int id = 1;
        bool success = false;
        HttpStatusCode codeExpected = HttpStatusCode.NotFound;
        string titleExpected = "Category update error";
        string messageExpected = string.Format("Category with ID {0} not found", id);
        CategoryRequest request = fixture.CategoryRequestMock();
        CategoryService service = fixture.WithGetCategory(id, success)
                                         .WithUpdateCategory()
                                         .InstantiateService();

        // Act
        CategoryException exception = await Assert.ThrowsAsync<CategoryException>(
            async () => await service.UpdateCategory(id, request)
        );

        // Assert
        Assert.Equal(codeExpected, exception.Code);
        Assert.Equal(titleExpected, exception.Title);
        Assert.Equal(messageExpected, exception.Message);
    }

    [Fact]
    public async Task MustEditCategoryAndReturnBadRequest()
    {
        // Arrange
        CategoryServiceFixture fixture = new();
        int id = 1;
        bool success = true;
        HttpStatusCode codeExpected = HttpStatusCode.BadRequest;
        string titleExpected = "Category update error";
        string messageExpected = "Invalid data!";
        string nameSqlInjection = "SELECT * FROM users; --";
        CategoryRequest request = fixture.CategoryRequestMock(name: nameSqlInjection);
        CategoryService service = fixture.WithGetCategory(id, success)
                                         .WithUpdateCategory()
                                         .InstantiateService();

        // Act
        CategoryException exception = await Assert.ThrowsAsync<CategoryException>(
            async () => await service.UpdateCategory(id, request)
        );

        // Assert
        Assert.Equal(codeExpected, exception.Code);
        Assert.Equal(titleExpected, exception.Title);
        Assert.Equal(messageExpected, exception.Message);
    }

    [Fact]
    public async Task MustDeleteCategoryAndReturnTrue()
    {
        // Arrange
        int id = 1;
        bool success = true;
        bool result = true;
        CategoryService service = new CategoryServiceFixture()
                                         .WithGetCategory(id, success)
                                         .WithDeleteCategory(result)
                                         .InstantiateService();

        // Act
        bool response = await service.DeleteCategory(id);

        // Assert
        Assert.True(response);
    }

    [Fact]
    public async Task MustDeleteCategoryAndReturnFalse()
    {
        // Arrange
        int id = 1;
        bool success = true;
        bool result = false;
        CategoryService service = new CategoryServiceFixture()
                                         .WithGetCategory(id, success)
                                         .WithDeleteCategory(result)
                                         .InstantiateService();

        // Act
        bool response = await service.DeleteCategory(id);

        // Assert
        Assert.False(response);
    }

    [Fact]
    public async Task MustDeleteCategoryAndReturnNotFound()
    {
        // Arrange
        int id = 1;
        bool success = false;
        HttpStatusCode codeExpected = HttpStatusCode.NotFound;
        string titleExpected = "Category delete error";
        string messageExpected = string.Format("Category with ID {0} not found", id);
        CategoryService service = new CategoryServiceFixture()
                                         .WithGetCategory(id, success)
                                         .InstantiateService();

        // Act
        CategoryException exception = await Assert.ThrowsAsync<CategoryException>(
            async () => await service.DeleteCategory(id)
        );

        // Assert
        Assert.Equal(codeExpected, exception.Code);
        Assert.Equal(titleExpected, exception.Title);
        Assert.Equal(messageExpected, exception.Message);
    }
}
