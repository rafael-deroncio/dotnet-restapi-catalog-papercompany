namespace PapperCompany.Catalog.Core.Services.Interfaces;

/// <summary>
/// Defines the contract for URI-related operations.
/// </summary>
public interface IUriService
{
    /// <summary>
    /// Retrieves the endpoint URI.
    /// </summary>
    /// <returns>The endpoint URI.</returns>
    Uri GetEndpoint();
}