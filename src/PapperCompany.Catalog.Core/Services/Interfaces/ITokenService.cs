using PapperCompany.Catalog.Domain.Requests;
using PapperCompany.Catalog.Domain.Responses;

namespace PapperCompany.Catalog.Core.Services.Interfaces;

public interface ITokenService
{
    Task<TokenResponse> Generate(TokenRequest request);
}
