using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace A01.CleanArchMvc.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    
    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategoriesAsync()
    {
        var categories = await _categoryService.GetCategoriesAsync();
        return Ok(categories);
    }
    
    [HttpGet("{id:int}", Name = "GetCategory")]
    public async Task<ActionResult<CategoryDto>> GetCategoryAsync(int? id)
    {
        var category = await _categoryService.GetByIdAsync(id);
        
        if (category is null)
            return NotFound("Categoria não encontrada");
        
        return Ok(category);
    }
    
    [HttpPost]
    public async Task<ActionResult> CreateAsync([FromBody] CategoryDto category)
    {
        if (category is null)
        {
            return BadRequest("Dados inválidos.");
        }

        await _categoryService.CreateAsync(category);

        return new CreatedAtRouteResult("GetCategory",new {id = category.Id}, category);
    }
    
    [HttpPut]
    public async Task<ActionResult<CategoryDto>> EditAsync(int id, [FromBody] CategoryDto category)
    {
        if (id == 0 || id != category?.Id)
            return BadRequest();
        
        await _categoryService.UpdateAsync(category);

        return Ok(category);
    }
    
    
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        var category = await _categoryService.GetByIdAsync(id);
        if (category is null)
            return NotFound("Categoria não encontrada");
        
        await _categoryService.RemoveAsync(id);
        return Ok();
    }
}