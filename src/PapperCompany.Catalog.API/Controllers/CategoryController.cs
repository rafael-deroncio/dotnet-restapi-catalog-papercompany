using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PapperCompany.Catalog.Core.Responses;
using PapperCompany.Catalog.Core.Services.Interfaces;
using PapperCompany.Catalog.Domain.Requests;
using PapperCompany.Catalog.Domain.Responses;

namespace PapperCompany.Catalog.API.COntrollers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
//[Authorize]
public class CategoryController(ICategoryService categoryService) : Controller
{
    private readonly ICategoryService _categoryService = categoryService;

    [HttpGet("paged")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCategories([FromQuery] PaginationRequest pagination)
        => Ok(await _categoryService.GetCategories(pagination));

    [HttpGet("{id:int}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCategory(int id)
        => Ok(await _categoryService.GetCategory(id));

    [HttpPost]
    //[Authorize(Roles = "Admin, Manager")]
    [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PostCategory([FromBody] CategoryRequest request)
        => Ok(await _categoryService.CreateCategory(request));

    [HttpPut("{id:int}")]
    //[Authorize(Roles = "Admin, Manager")]
    [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PutCategory(int id, [FromBody] CategoryRequest request)
        => Ok(await _categoryService.UpdateCategory(id, request));

    [HttpDelete("{id:int}")]
    //[Authorize(Roles = "Admin, Manager")]
    [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteCategory(int id)
        => Ok(await _categoryService.DeleteCategory(id));
}
