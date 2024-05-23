using System.Data;
using Dapper;
using PapperCompany.Catalog.Core.Arguments;
using PapperCompany.Catalog.Core.Models;
using PapperCompany.Catalog.Core.Repositories.Interfaces;

namespace PapperCompany.Catalog.Core.Repositories;

public class CategoryRepository(
    IConfiguration configuration,
    ILogger<CategoryRepository> logger) : BaseRepository(configuration), ICategoryRepository
{
    private readonly ILogger<CategoryRepository> _logger = logger;

    public async Task<IEnumerable<CategoryModel>> GetCategories(PaginationArgument argument)
    {
        try
        {
            using IDbConnection connection = GetConnection();
            string query = @"
                            SELECT 
                                C.CATEGORY_ID CategoryId,
                                C.NAME,
                                C.DESCRIPTION,
                                C.ACTIVE,
                                C.CREATED_AT CreatedAt,
                                C.UPDATED_AT UpdatedAt,
                                P.PRODUCT_ID ProductId,
                                P.NAME,
                                P.DESCRIPTION,
                                P.BRAND,
                                P.MODEL,
                                P.PRICE,
                                P.ACTIVE,
                                P.CREATED_AT CreatedAt,
                                P.UPDATED_AT UpdatedAt
                            FROM PRODUCTS P 
                            LEFT JOIN (
                                SELECT 
                                CATEGORY_ID,
                                NAME,
                                DESCRIPTION,
                                ACTIVE,
                                CREATED_AT,
                                UPDATED_AT
                                FROM CATEGORIES
                                OFFSET @Skip ROWS
                                FETCH NEXT @Size ROWS ONLY
                                ) C ON C.CATEGORY_ID = P.CATEGORY_ID
                            WHERE P.ACTIVE = true AND C.ACTIVE = true";

            Dictionary<int, CategoryModel> dataCategoryDict = [];

            IEnumerable<CategoryModel> result = await connection.QueryAsync<CategoryModel>(
                sql: query,
                param: argument,
                transaction: null,
                buffered: true,
                splitOn: "ProductId",
                types: [typeof(CategoryModel), typeof(ProductModel)],
                map: obj =>
                    {
                        CategoryModel temp;
                        CategoryModel category = obj[0] as CategoryModel;
                        ProductModel product = obj[1] as ProductModel;

                        if (!dataCategoryDict.TryGetValue(category.CategoryId, out temp))
                        {
                            temp = category;
                            temp.Products = new List<ProductModel>();
                            dataCategoryDict.Add(category.CategoryId, temp);
                        }

                        if (product != null) temp.Products.Add(product);

                        return temp;
                    }
                );

                return result.Distinct();
        }
        catch (Exception ex)
        {
            string message = string.Format("An error occurred while fetching categories. {0}", ex.Message);
            _logger.LogError(ex, message);
            throw;
        }
    }

    public async Task<CategoryModel> GetCategory(int id)
    {
        try
        {
            using IDbConnection connection = GetConnection();
            string query = @"
                        SELECT 
                            C.CATEGORY_ID CategoryId,
                            C.NAME,
                            C.DESCRIPTION,
                            C.ACTIVE,
                            C.CREATED_AT CreatedAt,
                            C.UPDATED_AT UpdatedAt,
                            P.PRODUCT_ID ProductId,
                            P.NAME,
                            P.DESCRIPTION,
                            P.BRAND,
                            P.MODEL,
                            P.PRICE,
							P.ACTIVE,
                            P.CREATED_AT CreatedAt,
                            P.UPDATED_AT UpdatedAt
                        FROM CATEGORIES C
                        LEFT JOIN PRODUCTS P ON P.CATEGORY_ID = C.CATEGORY_ID AND P.ACTIVE = true
                        WHERE C.ACTIVE = true
                        AND C.CATEGORY_ID = @Id";

            Dictionary<int, CategoryModel> dataCategoryDict = [];

            return (await connection.QueryAsync<CategoryModel>(
                sql: query,
                param: new { Id = id },
                transaction: null,
                buffered: true,
                splitOn: "ProductId",
                types: [typeof(CategoryModel), typeof(ProductModel)],
                map: obj =>
                    {
                        CategoryModel temp;
                        CategoryModel category = obj[0] as CategoryModel;
                        ProductModel product = obj[1] as ProductModel;

                        if (!dataCategoryDict.TryGetValue(category.CategoryId, out temp))
                        {
                            temp = category;
                            temp.Products = new List<ProductModel>();
                            dataCategoryDict.Add(category.CategoryId, temp);
                        }

                        if (product != null) temp.Products.Add(product);

                        return temp;
                    }
                )).FirstOrDefault();
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching categories. {0}", ex.Message);
            throw;
        }
    }

    public async Task<CategoryModel> CreateCategory(CategoryArgument argument)
    {
        try
        {
            using IDbConnection connection = GetConnection();
            string query = @"
                            INSERT INTO CATEGORIES (NAME, DESCRIPTION, ACTIVE, CREATED_AT, UPDATED_AT)
                            VALUES (@Name, @Description, @Active, @CreatedAt, @UpdatedAt)
                            RETURNING 
                                CATEGORY_ID AS CategoryId, 
                                NAME, 
                                DESCRIPTION, 
                                ACTIVE, 
                                CREATED_AT AS CreatedAt, 
                                UPDATED_AT AS UpdatedAt";

            return await connection.QueryFirstOrDefaultAsync<CategoryModel>(query, argument);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while creating category. {0}", ex.Message);
            throw;
        }
    }

    public async Task<CategoryModel> UpdateCategory(CategoryArgument argument)
    {
        try
        {
            using IDbConnection connection = GetConnection();
            string query = @"
                            UPDATE CATEGORIES
                            SET NAME = @Name, 
                                DESCRIPTION = @Description, 
                                UPDATED_AT = @UpdatedAt
                            WHERE CATEGORY_ID = @CategoryId
                            RETURNING 
                                CATEGORY_ID AS CategoryId, 
                                NAME, 
                                DESCRIPTION, 
                                ACTIVE, 
                                CREATED_AT AS CreatedAt, 
                                UPDATED_AT AS UpdatedAt";

            return await connection.QueryFirstOrDefaultAsync<CategoryModel>(query, argument);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while updating category. {0}", ex.Message);
            throw;
        }
    }

    public async Task<bool> DeleteCategory(int id)
    {
        try
        {
            using IDbConnection connection = GetConnection();
            string query = @"
                            UPDATE CATEGORIES SET ACTIVE = false WHERE CATEGORY_ID = @Id;
                            UPDATE PRODUCTS SET ACTIVE = false WHERE CATEGORY_ID = @Id;
                            ";

            return await connection.ExecuteAsync(query, new { Id = id }) > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while deleting category. {0}", ex.Message);
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
                            FROM CATEGORIES
                            WHERE ACTIVE = true";

            return await connection.ExecuteScalarAsync<int>(query);

        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while getting total registered categories. {0}", ex.Message);
            throw;
        }
    }
}
