using PapperCompany.Catalog.Core.Arguments;
using PapperCompany.Catalog.Core.Models;

namespace PapperCompany.Catalog.Core.Repositories.Interfaces;

/// <summary>
/// Defines the contract for product-related repository operations.
/// </summary>
public interface IProductRepository
{
    /// <summary>
    /// Retrieves the details of a specific product by its ID.
    /// </summary>
    /// <param name="id">The ID of the product to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the product details.</returns>
    Task<ProductModel> GetProduct(int id);

    /// <summary>
    /// Retrieves a list of products based on the specified pagination arguments.
    /// </summary>
    /// <param name="argument">The pagination argument containing page number and page size.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of product details.</returns>
    Task<IEnumerable<ProductModel>> GetProducts(PaginationArgument argument);

    /// <summary>
    /// Creates a new product based on the provided argument.
    /// </summary>
    /// <param name="argument">The argument containing details for the new product.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the details of the created product.</returns>
    Task<ProductModel> CreateProduct(ProductArgument argument);

    /// <summary>
    /// Updates an existing product based on the provided argument.
    /// </summary>
    /// <param name="argument">The argument containing updated details for the product.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated product details.</returns>
    Task<ProductModel> UpdateProduct(ProductArgument argument);

    /// <summary>
    /// Deletes a product by its ID.
    /// </summary>
    /// <param name="id">The ID of the product to delete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the deletion was successful.</returns>
    Task<bool> DeleteProduct(int id);

    /// <summary>
    /// Retrieves the total number of product records.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the total number of product records.</returns>
    Task<int> GetTotalRecords();
}
