﻿using PapperCompany.Catalog.Core.Arguments;
using PapperCompany.Catalog.Core.Models;

namespace PapperCompany.Catalog.Core.Repositories.Interfaces;

public interface ICategoryRepository
{
    Task<CategoryModel> Get(int id);
    Task<IEnumerable<CategoryModel>> GetCategories(PaginationArgument argument);
    Task<CategoryModel> CreateCategory(CategoryArgument argument);
    Task<CategoryModel> UpdateCategory(CategoryArgument argument);
    Task<bool> DeleteCategory(int id);

    Task<int> GetTotalRecords();
}
