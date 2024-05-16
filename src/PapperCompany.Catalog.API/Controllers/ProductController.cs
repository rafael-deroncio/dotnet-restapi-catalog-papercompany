using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PapperCompany.Catalog.Domain;
using PapperCompany.Catalog.Domain.Responses;

namespace PapperCompany.Catalog.API.COntrollers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
[Authorize]
public class ProductController : Controller
{
    public ProductController()
    {

    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetProducts() => Ok(await Task.FromResult(0));

    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetProduct(int id) => Ok(await Task.FromResult(0));

    [HttpPost]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> PostProduct() => Ok(await Task.FromResult(0));

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> PutProduct(int id) => Ok(await Task.FromResult(0));

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> DeleteProduct(int id) => Ok(await Task.FromResult(0));
}
