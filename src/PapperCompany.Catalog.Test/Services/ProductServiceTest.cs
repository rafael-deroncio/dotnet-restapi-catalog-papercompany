using System.Net;
using PapperCompany.Catalog.Core.Exceptions;
using PapperCompany.Catalog.Core.Services;
using PapperCompany.Catalog.Domain.Requests;
using PapperCompany.Catalog.Domain.Responses;
using PapperCompany.Catalog.Test.Fistures;

namespace PapperCompany.Catalog.Test.Services;

public class ProductServiceTest
{
    [Fact]
    public async Task MustGetProductSuccessfully()
    {
        // Arrange
        int id = 1;
        bool success = true;
        ProductService fixture = new ProductServiceFixture()
                                      .WithGetProduct(id, success)
                                      .WithMapModelToResponse()
                                      .InstantiateService();

        // Act
        ProductResponse response = await fixture.GetProduct(id);

        // Assert
        Assert.NotNull(response);
    }

    [Fact]
    public async Task MustGetProductAndReturnNotFound()
    {
        // Arrange
        int id = 1;
        bool success = false;
        string message = string.Format("Product with ID {0} not found", id);
        HttpStatusCode code = HttpStatusCode.NotFound;
        ProductService fixture = new ProductServiceFixture()
                                      .WithGetProduct(id, success)
                                      .WithMapModelToResponse()
                                      .InstantiateService();

        // Act
        ProductException exception = await Assert.ThrowsAsync<ProductException>(async () =>
        {
            await fixture.GetProduct(id);
        });

        // Assert
        Assert.NotNull(exception);
        Assert.Equal(message, exception.Message);
        Assert.Equal(code, exception.Code);
    }

    [Fact]
    public async Task MustGetProductsSuccessfully()
    {
        // Arrange
        ProductServiceFixture fixture = new ProductServiceFixture();
        PaginationRequest request = fixture.PaginationRequestMock();
        ProductService service = fixture.WithGetProducts()
                                         .WithGetPagination()
                                         .WithGetTotalRecords()
                                         .WithMapPaginationRequestToArgument()
                                         .WithMapArgumentToRequest()
                                         .WithMapModelToResponseList()
                                         .InstantiateService();

        // Act
        PaginationResponse<ProductResponse> response = await service.GetProducts(request);

        // Assert
        Assert.True(response.PageNumber > 0);
        Assert.True(response.PageSize > 0);
        Assert.NotNull(response.Data);
    }

    [Fact]
    public async Task MustCreateNewProductSuccessfully()
    {
        // Arrange
        int id = 1;
        bool success = true;
        ProductServiceFixture fixture = new();
        ProductRequest request = fixture.ProductRequestMock(categoryId: id);
        ProductService service = fixture.WithCreateProduct(id)
                                        .WithGetCategory(id, success)
                                        .WithMapProductRequestToArgument()
                                        .WithMapModelToResponse()
                                        .InstantiateService();

        // Act
        ProductResponse response = await service.CreateProduct(request);

        // Assert
        Assert.NotNull(response);
    }

    [Fact]
    public async Task MustCreateNewProductAndReturnBadRequestCategoryNotFound()
    {
        // Arrange
        int id = 1;
        bool success = false;
        string message = string.Format("Product category with ID {0} not found!", id);
        HttpStatusCode code = HttpStatusCode.NotFound;
        ProductServiceFixture fixture = new();
        ProductRequest request = fixture.ProductRequestMock(categoryId: id);
        ProductService service = fixture.WithCreateProduct(id)
                                        .WithGetCategory(id, success)
                                        .InstantiateService();

        // Act
        ProductException exception = await Assert.ThrowsAsync<ProductException>(
            async () => await service.CreateProduct(request)
        );

        // Assert
        Assert.NotNull(exception);
        Assert.Equal(message, exception.Message);
        Assert.Equal(code, exception.Code);
    }

    [Fact]
    public async Task MustCreateNewProductAndReturnBadRequest()
    {
        // Arrange
        int id = 1;
        string sqlInjection = "SELECT * FROM users; --";
        string message = "Invalid data!";
        HttpStatusCode code = HttpStatusCode.BadRequest;
        ProductServiceFixture fixture = new();
        ProductRequest request = fixture.ProductRequestMock(categoryId: id, name: sqlInjection);
        ProductService service = fixture.InstantiateService();

        // Act
        ProductException exception = await Assert.ThrowsAsync<ProductException>(
            async () => await service.CreateProduct(request)
        );

        // Assert
        Assert.NotNull(exception);
        Assert.Equal(message, exception.Message);
        Assert.Equal(code, exception.Code);
    }

    [Fact]
    public async Task MustEditProductSuccessfully()
    {
        // Arrange
        ProductServiceFixture fixture = new();
        int id = 1;
        bool success = true;
        ProductRequest request = fixture.ProductRequestMock(categoryId: id);
        ProductService service = fixture.WithGetProduct(id)
                                        .WithGetCategory(id, success)
                                        .WithUpdateProduct()
                                        .WithMapProductRequestToArgument()
                                        .WithMapModelToResponse()
                                        .InstantiateService();

        // Act
        ProductResponse response = await service.UpdateProduct(id, request);

        // Assert
        Assert.NotNull(response);
    }

    [Fact]
    public async Task MustEditProductAndReturnNotFound()
    {
        // Arrange
        int id = 1;
        bool success = false;
        string message = string.Format("Product with ID {0} not found!", id);
        HttpStatusCode code = HttpStatusCode.NotFound;
        ProductServiceFixture fixture = new();
        ProductRequest request = fixture.ProductRequestMock(categoryId: id);
        ProductService service = fixture.WithGetProduct(id, success)
                                        .InstantiateService();

        // Act
        ProductException exception = await Assert.ThrowsAsync<ProductException>(
            async () => await service.UpdateProduct(id, request)
        );

        // Assert
        Assert.NotNull(exception);
        Assert.Equal(message, exception.Message);
        Assert.Equal(code, exception.Code);
    }

    [Fact]
    public async Task MustEditProductAndReturnBadRequest()
    {
        // Arrange
        int id = 1;
        string sqlInjection = "SELECT * FROM users; --";
        string message = "Invalid data!";
        HttpStatusCode code = HttpStatusCode.BadRequest;
        ProductServiceFixture fixture = new();
        ProductRequest request = fixture.ProductRequestMock(name: sqlInjection);
        ProductService service = fixture.InstantiateService();

        // Act
        ProductException exception = await Assert.ThrowsAsync<ProductException>(
            async () => await service.UpdateProduct(id, request)
        );

        // Assert
        Assert.NotNull(exception);
        Assert.Equal(message, exception.Message);
        Assert.Equal(code, exception.Code);
    }

    [Fact]
    public async Task MustDeleteProductAndReturnTrue()
    {
        // Arrange
        int id = 1;
        bool success = true;
        bool result = true;
        ProductService service = new ProductServiceFixture()
                                         .WithGetProduct(id, success)
                                         .WithDeleteProduct(result)
                                         .InstantiateService();

        // Act
        bool response = await service.DeleteProduct(id);

        // Assert
        Assert.True(response);
    }

    [Fact]
    public async Task MustDeleteProductAndReturnFalse()
    {
        // Arrange
        int id = 1;
        bool success = true;
        bool result = false;
        ProductService service = new ProductServiceFixture()
                                         .WithGetProduct(id, success)
                                         .WithDeleteProduct(result)
                                         .InstantiateService();

        // Act
        bool response = await service.DeleteProduct(id);

        // Assert
        Assert.False(response);
    }

    [Fact]
    public async Task MustDeleteProductAndReturnNotFound()
    {
        // Arrange
        int id = 1;
        bool success = false;
        HttpStatusCode code = HttpStatusCode.NotFound;
        string message = string.Format("Product with ID {0} not found!", id);

        ProductService service = new ProductServiceFixture()
                                         .WithGetCategory(id, success)
                                         .InstantiateService();

        // Act
        ProductException exception = await Assert.ThrowsAsync<ProductException>(
            async () => await service.DeleteProduct(id)
        );

        // Assert
        Assert.NotNull(exception);
        Assert.Equal(message, exception.Message);
        Assert.Equal(code, exception.Code);
    }
}
