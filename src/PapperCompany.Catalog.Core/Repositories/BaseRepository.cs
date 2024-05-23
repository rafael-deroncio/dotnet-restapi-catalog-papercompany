using System.Data;
using Npgsql;
using PapperCompany.Catalog.Core.Repositories.Interfaces;

namespace PapperCompany.Catalog.Core;

public abstract class BaseRepository(IConfiguration configuration) : IBaseRepository
{
    private readonly IConfiguration _configuration = configuration;

    public IDbConnection GetConnection()
    {
        string connectionString = _configuration["DBCatalogConnectionString"];
        NpgsqlConnection connection = new(connectionString);
        connection.Open();
        return connection;
    }

    public async Task<IDbConnection> GetConnectionAsync()
    {
        string connectionString = _configuration["DBCatalogConnectionString"];
        NpgsqlConnection connection = new(connectionString);
        await connection.OpenAsync();
        return connection;
    }

    public abstract Task<int> GetTotalRecords();
}
