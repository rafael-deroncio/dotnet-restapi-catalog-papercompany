using System.Data;

namespace PapperCompany.Catalog.Core.Repositories.Interfaces;

public interface IBaseRepository
{
    IDbConnection GetConnection();
    Task<IDbConnection> GetConnectionAsync();
}
