using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace A01.CleanArchMvc.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetProductsAsync()
    {
        var products = await _productService.GetProductsAsync();
        return Ok(products);
    }
    
    [HttpGet("{id:int}", Name = "GetProduct")]
    public async Task<IActionResult> GetProductAsync(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        
        if (product is null)
            return NotFound("Produto não encontrado");
        
        return Ok(product);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync(ProductDto product)
    {
        if (product is null)
        {
            return BadRequest("Dados inválidos.");
        }

        await _productService.CreateAsync(product);

        return new CreatedAtRouteResult("GetProduct",new {id = product.Id}, product);
    }
    
    [HttpPut]
    public async Task<ActionResult<CategoryDto>> EditAsync(int id, [FromBody] ProductDto product)
    {
        if (id == 0 || id != product?.Id)
            return BadRequest();
        
        await _productService.UpdateAsync(product);

        return Ok(product);
    }
    
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product is null)
            return NotFound("Produto não encontrado");
        
        await _productService.RemoveAsync(id);
        return Ok();
    }
}