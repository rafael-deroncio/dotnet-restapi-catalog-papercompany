using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PapperCompany.Catalog.API.COntrollers;


[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
[Authorize]
public class CategoryController : Controller
{
    public CategoryController()
    {

    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetCategories() => Ok(await Task.FromResult(0));

    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetCategory(int id) => Ok(await Task.FromResult(0));

    [HttpPost]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> PostCategory() => Ok(await Task.FromResult(0));

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> PutCategory(int id) => Ok(await Task.FromResult(0));

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> DeleteCategory(int id) => Ok(await Task.FromResult(0));
}
