using PapperCompany.Catalog.Domain.Requests;
using PapperCompany.Catalog.Domain.Responses;

namespace PapperCompany.Catalog.Core.Services.Interfaces;

public interface IPaginationService
{
    Task<PaginationResponse<T>> GetPagination<T>(PaginationRequest request, int total, T content);
}
