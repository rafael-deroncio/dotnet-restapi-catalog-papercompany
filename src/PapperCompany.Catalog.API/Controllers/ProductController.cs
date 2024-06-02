using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PapperCompany.Catalog.Core.Services.Interfaces;
using PapperCompany.Catalog.Domain.Requests;
using PapperCompany.Catalog.Domain.Responses;

namespace PapperCompany.Catalog.API.COntrollers;

/// <summary>
/// API controller for managing products.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="ProductController"/> class.
/// </remarks>
/// <param name="productService">The product service.</param>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
[Authorize]
public class ProductController(IProductService productService) : Controller
{
    private readonly IProductService _productService = productService;

    /// <summary>
    /// Retrieves a paginated list of products.
    /// </summary>
    /// <param name="pagination">The pagination parameters.</param>
    /// <returns>A paginated list of products.</returns>
    [HttpGet("paged")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(PaginationResponse<ProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetProducts([FromQuery] PaginationRequest pagination)
         => Ok(await _productService.GetProducts(pagination));

    /// <summary>
    /// Retrieves the details of a specific product.
    /// </summary>
    /// <param name="id">The product ID.</param>
    /// <returns>The details of the product.</returns>
    [HttpGet("{id:int}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetProduct(int id)
        => Ok(await _productService.GetProduct(id));

    /// <summary>
    /// Creates a new product.
    /// </summary>
    /// <param name="request">The product data.</param>
    /// <returns>The created product.</returns>
    [HttpPost]
    //[Authorize(Roles = "Admin, Manager")]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PostProduct([FromBody] ProductRequest request)
        => Ok(await _productService.CreateProduct(request));

    /// <summary>
    /// Updates an existing product.
    /// </summary>
    /// <param name="id">The product ID.</param>
    /// <param name="request">The updated product data.</param>
    /// <returns>The updated product.</returns>
    [HttpPut("{id:int}")]
    //[Authorize(Roles = "Admin, Manager")]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PutProduct(int id, [FromBody] ProductRequest request)
        => Ok(await _productService.UpdateProduct(id, request));

    /// <summary>
    /// Deletes a product.
    /// </summary>
    /// <param name="id">The product ID.</param>
    /// <returns>True if deletion was successful, otherwise false.</returns>
    [HttpDelete("{id:int}")]
    //[Authorize(Roles = "Admin, Manager")]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteProduct(int id)
        => Ok(await _productService.DeleteProduct(id));
}
