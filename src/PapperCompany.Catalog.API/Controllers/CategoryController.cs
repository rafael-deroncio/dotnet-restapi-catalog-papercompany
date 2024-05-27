using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PapperCompany.Catalog.Core.Responses;
using PapperCompany.Catalog.Core.Services.Interfaces;
using PapperCompany.Catalog.Domain.Requests;
using PapperCompany.Catalog.Domain.Responses;

namespace PapperCompany.Catalog.API.COntrollers;

/// <summary>
/// API controller for managing categories.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="CategoryController"/> class.
/// </remarks>
/// <param name="categoryService">The category service.</param>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
//[Authorize]
public class CategoryController(ICategoryService categoryService) : Controller
{
    private readonly ICategoryService _categoryService = categoryService;

    /// <summary>
    /// Retrieves a paginated list of categories.
    /// </summary>
    /// <param name="pagination">The pagination parameters.</param>
    /// <returns>A paginated list of categories.</returns>
    [HttpGet("paged")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(PaginationResponse<CategoryResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCategories([FromQuery] PaginationRequest pagination)
        => Ok(await _categoryService.GetCategories(pagination));

    /// <summary>
    /// Retrieves the details of a specific category.
    /// </summary>
    /// <param name="id">The category ID.</param>
    /// <returns>The details of the category.</returns>
    [HttpGet("{id:int}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCategory(int id)
        => Ok(await _categoryService.GetCategory(id));

    /// <summary>
    /// Creates a new category.
    /// </summary>
    /// <param name="request">The category data.</param>
    /// <returns>The created category.</returns>
    [HttpPost]
    //[Authorize(Roles = "Admin, Manager")]
    [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PostCategory([FromBody] CategoryRequest request)
        => Ok(await _categoryService.CreateCategory(request));

    /// <summary>
    /// Updates an existing category.
    /// </summary>
    /// <param name="id">The category ID.</param>
    /// <param name="request">The updated category data.</param>
    /// <returns>The updated category.</returns>
    [HttpPut("{id:int}")]
    //[Authorize(Roles = "Admin, Manager")]
    [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PutCategory(int id, [FromBody] CategoryRequest request)
        => Ok(await _categoryService.UpdateCategory(id, request));

    /// <summary>
    /// Deletes a category.
    /// </summary>
    /// <param name="id">The category ID.</param>
    /// <returns>True if deletion was successful, otherwise false.</returns>
    [HttpDelete("{id:int}")]
    //[Authorize(Roles = "Admin, Manager")]
    [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteCategory(int id)
        => Ok(await _categoryService.DeleteCategory(id));
}
