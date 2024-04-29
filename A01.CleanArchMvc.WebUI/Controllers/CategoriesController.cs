using System;
using System.Threading.Tasks;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.WebUI.Controllers;

[Authorize(Roles = "Admin")]
public class CategoriesController : Controller
{
    private readonly ICategoryService _categoryService;
    
    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var categories = await _categoryService.GetCategoriesAsync();
        return View(categories);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CategoryDto category)
    {
        if (ModelState.IsValid)
        {
            await _categoryService.CreateAsync(category);
            return RedirectToAction(nameof(Index));
        }
        
        return View(category);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        var categoryDto = await _categoryService.GetByIdAsync(id);
        
        if (categoryDto is null)
            return NotFound();
        
        return View(categoryDto);
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(CategoryDto category)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _categoryService.UpdateAsync(category);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(category);
    }
    
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        var categoryDto = await _categoryService.GetByIdAsync(id);
        
        if (categoryDto is null)
            return NotFound();
        
        return View(categoryDto);
    }
    
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _categoryService.RemoveAsync(id);
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public async Task<IActionResult> Details(int? id)
    {
        var categoryDto = await _categoryService.GetByIdAsync(id);
        
        if (categoryDto is null)
            return NotFound();
        
        return View(categoryDto);
    }
}