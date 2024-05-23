using PapperCompany.Catalog.Core.Arguments;
using PapperCompany.Catalog.Core.Models;

namespace PapperCompany.Catalog.Core.Repositories.Interfaces;

/// <summary>
/// Defines the contract for category-related repository operations.
/// </summary>
public interface ICategoryRepository
{
    /// <summary>
    /// Retrieves the details of a specific category by its ID.
    /// </summary>
    /// <param name="id">The ID of the category to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the category details.</returns>
    Task<CategoryModel> GetCategory(int id);

    /// <summary>
    /// Retrieves a list of categories based on the specified pagination arguments.
    /// </summary>
    /// <param name="argument">The pagination argument containing page number and page size.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of category details.</returns>
    Task<IEnumerable<CategoryModel>> GetCategories(PaginationArgument argument);

    /// <summary>
    /// Creates a new category based on the provided argument.
    /// </summary>
    /// <param name="argument">The argument containing details for the new category.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the details of the created category.</returns>
    Task<CategoryModel> CreateCategory(CategoryArgument argument);

    /// <summary>
    /// Updates an existing category based on the provided argument.
    /// </summary>
    /// <param name="argument">The argument containing updated details for the category.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated category details.</returns>
    Task<CategoryModel> UpdateCategory(CategoryArgument argument);

    /// <summary>
    /// Deletes a category by its ID.
    /// </summary>
    /// <param name="id">The ID of the category to delete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the deletion was successful.</returns>
    Task<bool> DeleteCategory(int id);

    /// <summary>
    /// Retrieves the total number of category records.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the total number of category records.</returns>
    Task<int> GetTotalRecords();
}
