using System.Data;
using Dapper;
using PapperCompany.Catalog.Core.Arguments;
using PapperCompany.Catalog.Core.Models;
using PapperCompany.Catalog.Core.Repositories.Interfaces;

namespace PapperCompany.Catalog.Core.Repositories;

public class ProductRepository(
    IConfiguration configuration,
    ILogger<ProductRepository> logger) : BaseRepository(configuration), IProductRepository
{
    private readonly ILogger<ProductRepository> _logger = logger;

    public async Task<IEnumerable<ProductModel>> GetProducts(PaginationArgument argument)
    {
        try
        {
            using IDbConnection connection = GetConnection();
            string query = @"
                            SELECT 
                                P.PRODUCT_ID ProductId,
                                P.NAME,
                                P.DESCRIPTION,
                                P.BRAND,
                                P.MODEL,
                                P.PRICE,
                                P.ACTIVE,
                                P.CREATED_AT CreatedAt,
                                P.UPDATED_AT UpdatedAt,
                                C.CATEGORY_ID CategoryId,
                                C.NAME,
                                C.DESCRIPTION,
                                C.ACTIVE,
                                C.CREATED_AT CreatedAt,
                                C.UPDATED_AT UpdatedAt
                            FROM PRODUCTS P 
                            JOIN CATEGORIES C ON C.CATEGORY_ID = P.CATEGORY_ID
                            WHERE P.ACTIVE = true AND C.ACTIVE = true
                            OFFSET @Skip ROWS
                            FETCH NEXT @Size ROWS ONLY";

            Dictionary<int, ProductModel> dataProductDict = [];

            return await connection.QueryAsync<ProductModel>(
                sql: query,
                param: argument,
                transaction: null,
                buffered: true,
                splitOn: "CategoryId",
                types: [typeof(CategoryModel), typeof(ProductModel)],
                map: obj =>
                    {
                        ProductModel temp;
                        ProductModel product = obj[0] as ProductModel;
                        CategoryModel category = obj[1] as CategoryModel;

                        if (!dataProductDict.TryGetValue(product.ProductId, out temp))
                        {
                            temp = product;
                            temp.Category = new();
                            dataProductDict.Add(product.ProductId, temp);
                        }

                        if (category != null) temp.Category = category;

                        return temp;
                    }
                );
        }
        catch (Exception ex)
        {
            string message = string.Format("An error occurred while fetching products. {0}", ex.Message);
            _logger.LogError(ex, message);
            throw;
        }
    }

    public async Task<ProductModel> GetProduct(int id)
    {
        try
        {
            using IDbConnection connection = GetConnection();
            string query = @"
                        SELECT 
                            P.PRODUCT_ID ProductId,
                            P.NAME,
                            P.DESCRIPTION,
                            P.BRAND,
                            P.MODEL,
                            P.PRICE,
							P.ACTIVE,
                            P.CREATED_AT CreatedAt,
                            P.UPDATED_AT UpdatedAt,
                            C.CATEGORY_ID CategoryId,
                            C.NAME,
                            C.DESCRIPTION,
                            C.ACTIVE,
                            C.CREATED_AT CreatedAt,
                            C.UPDATED_AT UpdatedAt
                        FROM PRODUCTS P
                        LEFT JOIN CATEGORIES C ON C.CATEGORY_ID = P.CATEGORY_ID
                        WHERE C.ACTIVE = true AND P.ACTIVE = true
                        AND P.PRODUCT_ID = @Id";

            Dictionary<int, ProductModel> dataProductDict = [];

            return (await connection.QueryAsync<ProductModel>(
                sql: query,
                param: new { Id = id },
                transaction: null,
                buffered: true,
                splitOn: "CategoryId",
                types: [typeof(ProductModel), typeof(CategoryModel)],
                map: obj =>
                    {
                        ProductModel temp;
                        ProductModel product = obj[0] as ProductModel;
                        CategoryModel category = obj[1] as CategoryModel;

                        if (!dataProductDict.TryGetValue(product.ProductId, out temp))
                        {
                            temp = product;
                            temp.Category = new();
                            dataProductDict.Add(product.ProductId, temp);
                        }

                        if (category != null) temp.Category = category;

                        return temp;
                    }
                )).FirstOrDefault();
        }
        catch (Exception ex)
        {
            string message = string.Format("An error occurred while fetching product. {0}", ex.Message);
            _logger.LogError(ex, message);
            throw;
        }
    }

    public async Task<ProductModel> CreateProduct(ProductArgument argument)
    {
        try
        {
            using IDbConnection connection = GetConnection();
            string query = @"
                            INSERT INTO PRODUCTS (NAME, DESCRIPTION, BRAND, MODEL, PRICE, CATEGORY_ID, ACTIVE, CREATED_AT, UPDATED_AT)
                            VALUES (@Name, @Description, @Brand, @Model, @Price, @CategoryId, @Active, @CreatedAt, @UpdatedAt)
                            RETURNING PRODUCT_ID";

            return await GetProduct(
                await connection.QueryFirstOrDefaultAsync<int>(query, argument)
            );
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while creating product. {0}", ex.Message);
            throw;
        }
    }

    public async Task<ProductModel> UpdateProduct(ProductArgument argument)
    {
        try
        {
            using IDbConnection connection = GetConnection();
            string query = @"
                            UPDATE PRODUCTS
                            SET NAME = @Name, 
                                DESCRIPTION = @Description, 
                                BRAND = @Brand, 
                                MODEL = @Model, 
                                PRICE = @Price, 
                                CATEGORY_ID = @CategoryId,
                                UPDATED_AT = @UpdatedAt
                            WHERE PRODUCT_ID = @ProductId
                            RETURNING 
                                PRODUCT_ID";

            return await GetProduct(
                await connection.QueryFirstOrDefaultAsync<int>(
                    query,
                    new
                    {
                        argument.Name,
                        argument.Description,
                        argument.Model,
                        argument.Brand,
                        argument.Price,
                        argument.Category.CategoryId,
                        argument.UpdatedAt
                    })
            );
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while updating product. {0}", ex.Message);
            throw;
        }
    }

    public async Task<bool> DeleteProduct(int id)
    {
        try
        {
            using IDbConnection connection = GetConnection();
            string query = @"UPDATE PRODUCTS SET ACTIVE = false WHERE PRODUCT_ID = @Id;";

            return await connection.ExecuteAsync(query, new { Id = id }) > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while deleting product. {0}", ex.Message);
            throw;
        }
    }

    public override async Task<int> GetTotalRecords()
    {
        try
        {
            using IDbConnection connection = GetConnection();
            string query = @"
                            SELECT Count(*) 
                            FROM PRODUCTS
                            WHERE ACTIVE = true";

            return await connection.ExecuteScalarAsync<int>(query);

        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while getting total registered products. {0}", ex.Message);
            throw;
        }
    }
}
