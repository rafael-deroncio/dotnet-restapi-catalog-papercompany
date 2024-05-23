using PapperCompany.Catalog.Domain.Requests;
using PapperCompany.Catalog.Domain.Responses;

namespace PapperCompany.Catalog.Core.Services.Interfaces;

/// <summary>
/// Defines the contract for category-related operations.
/// </summary>
public interface ICategoryService
{
    /// <summary>
    /// Retrieves the details of a specific category by its ID.
    /// </summary>
    /// <param name="id">The ID of the category to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the category details.</returns>
    Task<CategoryResponse> GetCategory(int id);

    /// <summary>
    /// Retrieves a paginated list of categories based on the specified pagination request.
    /// </summary>
    /// <param name="request">The pagination request containing page number and page size.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of category details.</returns>
    Task<PaginationResponse<IEnumerable<CategoryResponse>>> GetCategories(PaginationRequest request);

    /// <summary>
    /// Creates a new category based on the provided request.
    /// </summary>
    /// <param name="request">The request containing details for the new category.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the details of the created category.</returns>
    Task<CategoryResponse> CreateCategory(CategoryRequest request);

    /// <summary>
    /// Updates an existing category based on the provided ID and request details.
    /// </summary>
    /// <param name="id">The ID of the category to update.</param>
    /// <param name="request">The request containing updated details for the category.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated category details.</returns>
    Task<CategoryResponse> UpdateCategory(int id, CategoryRequest request);

    /// <summary>
    /// Deletes a category by its ID.
    /// </summary>
    /// <param name="id">The ID of the category to delete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the deletion was successful.</returns>
    Task<bool> DeleteCategory(int id);
}
