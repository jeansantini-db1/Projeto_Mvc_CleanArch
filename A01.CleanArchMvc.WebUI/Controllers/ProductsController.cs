using System;
using System.IO;
using System.Threading.Tasks;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CleanArchMvc.WebUI.Controllers;

[Authorize]
public class ProductsController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IWebHostEnvironment _environment;
    
    public ProductsController(IProductService productService, ICategoryService categoryService, 
        IWebHostEnvironment environment)
    {
        _productService = productService;
        _categoryService = categoryService;
        _environment = environment;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetProductsAsync();
        return View(products);
    }
    
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewBag.CategoryId = new SelectList(await _categoryService.GetCategoriesAsync(), "Id", "Name");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductDto product)
    {
        if (ModelState.IsValid)
        {
            await _productService.CreateAsync(product);
            return RedirectToAction(nameof(Index));
        }
        
        return View(product);
    }
    
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        var productDto = await _productService.GetByIdAsync(id);

        if (productDto is null)
            return NotFound();
        
        ViewBag.CategoryId = new SelectList(await _categoryService.GetCategoriesAsync(), "Id", "Name", productDto.CategoryId);
        
        return View(productDto);
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(ProductDto product)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _productService.UpdateAsync(product);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }
    
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        var productDto = await _productService.GetByIdAsync(id);
        
        if (productDto is null)
            return NotFound();
        
        return View(productDto);
    }
    
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _productService.RemoveAsync(id);
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public async Task<IActionResult> Details(int? id)
    {
        var productDto = await _productService.GetByIdAsync(id);
        
        if (productDto is null)
            return NotFound();

        var wwwroot = _environment.WebRootPath;
        var image = Path.Combine(wwwroot, "images\\" + productDto.Image);
        var exists = System.IO.File.Exists(image);
        ViewBag.ImageExist = exists;
        
        return View(productDto);
    }
}