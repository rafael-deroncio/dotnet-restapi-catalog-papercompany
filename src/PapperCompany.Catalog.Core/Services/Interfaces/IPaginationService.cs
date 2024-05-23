using PapperCompany.Catalog.Domain.Requests;
using PapperCompany.Catalog.Domain.Responses;

namespace PapperCompany.Catalog.Core.Services.Interfaces;

/// <summary>
/// Defines the contract for pagination-related operations.
/// </summary>
public interface IPaginationService
{
    /// <summary>
    /// Retrieves a paginated response based on the specified pagination request, total record count, and content.
    /// </summary>
    /// <typeparam name="T">The type of the content contained in the paginated response.</typeparam>
    /// <param name="request">The pagination request containing page number and page size.</param>
    /// <param name="total">The total number of records available.</param>
    /// <param name="content">The content to be included in the paginated response.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the paginated response.</returns>
    Task<PaginationResponse<T>> GetPagination<T>(PaginationRequest request, int total, T content);
}
