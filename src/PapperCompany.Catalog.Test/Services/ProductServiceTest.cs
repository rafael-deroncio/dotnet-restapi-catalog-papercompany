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
        Assert.Equal(HttpStatusCode.NotFound, exception.Code);
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
                                         .WithMapRequestToArgument()
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
    public async Task MustCreateNewCategorySuccessfully()
    {
        // Arrange

        // Act

        // Assert
        Assert.True(true);
    }

    [Fact]
    public async Task MustCreateNewCategoryAndReturnBadRequest()
    {
        // Arrange

        // Act

        // Assert
        Assert.True(true);
    }

    [Fact]
    public async Task MustEditCategorySuccessfully()
    {
        // Arrange

        // Act

        // Assert
        Assert.True(true);
    }

    [Fact]
    public async Task MustEditCategoryAndReturnNotFound()
    {
        // Arrange

        // Act

        // Assert
        Assert.True(true);
    }

    [Fact]
    public async Task MustEditCategoryAndReturnBadRequest()
    {
        // Arrange

        // Act

        // Assert
        Assert.True(true);
    }

    [Fact]
    public async Task MustDeleteCategoryAndReturnTrue()
    {
        // Arrange

        // Act

        // Assert
        Assert.True(true);
    }

    [Fact]
    public async Task MustDeleteCategoryAndReturnFalse()
    {
        // Arrange

        // Act

        // Assert
        Assert.True(true);
    }

    [Fact]
    public async Task MustDeleteCategoryAndReturnNotFound()
    {
        // Arrange

        // Act

        // Assert
        Assert.True(true);
    }
}
