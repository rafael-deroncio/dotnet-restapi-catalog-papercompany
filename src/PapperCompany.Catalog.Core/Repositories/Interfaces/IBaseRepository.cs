using System.Data;

namespace PapperCompany.Catalog.Core.Repositories.Interfaces;

/// <summary>
/// Defines the contract for base repository operations, providing methods for retrieving database connections.
/// </summary>
public interface IBaseRepository
{
    /// <summary>
    /// Retrieves a database connection synchronously.
    /// </summary>
    /// <returns>The database connection.</returns>
    IDbConnection GetConnection();

    /// <summary>
    /// Retrieves a database connection asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the database connection.</returns>
    Task<IDbConnection> GetConnectionAsync();
}
