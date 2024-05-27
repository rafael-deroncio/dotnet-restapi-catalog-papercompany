using PapperCompany.Catalog.Domain.Requests;
using PapperCompany.Catalog.Domain.Responses;

namespace PapperCompany.Catalog.Core.Services.Interfaces;

/// <summary>
/// Defines the contract for product-related operations.
/// </summary>
public interface IProductService
{
    /// <summary>
    /// Retrieves the details of a specific product by its ID.
    /// </summary>
    /// <param name="id">The ID of the product to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the product details.</returns>
    Task<ProductResponse> GetProduct(int id);

    /// <summary>
    /// Retrieves a paginated list of products based on the specified pagination request.
    /// </summary>
    /// <param name="request">The pagination request containing page number and page size.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of product details.</returns>
    Task<PaginationResponse<ProductResponse>> GetProducts(PaginationRequest request);

    /// <summary>
    /// Creates a new product based on the provided request.
    /// </summary>
    /// <param name="request">The request containing details for the new product.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the details of the created product.</returns>
    Task<ProductResponse> CreateProduct(ProductRequest request);

    /// <summary>
    /// Updates an existing product based on the provided ID and request details.
    /// </summary>
    /// <param name="id">The ID of the product to update.</param>
    /// <param name="request">The request containing updated details for the product.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated product details.</returns>
    Task<ProductResponse> UpdateProduct(int id, ProductRequest request);

    /// <summary>
    /// Deletes a product by its ID.
    /// </summary>
    /// <param name="id">The ID of the product to delete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the deletion was successful.</returns>
    Task<bool> DeleteProduct(int id);
}
