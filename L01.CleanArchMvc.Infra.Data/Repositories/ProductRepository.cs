using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchMvc.Infra.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _productContext;

    public ProductRepository(ApplicationDbContext productContext)
    {
        _productContext = productContext;
    }
    
    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        return await _productContext.Product.ToListAsync();
    }

    public async Task<Product> GetByIdAsync(int? id)
    {
        //eager loading (include)
        return await _productContext.Product.Include(p => p.Category)
            .SingleOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Product> GetByCategoryAsync(int? id)
    {
        return await _productContext.Product
            .Where(p => p.CategoryId == id).FirstOrDefaultAsync();
    }

    public async Task<Product> CreateAsync(Product product)
    {
        _productContext.Add(product);
        await _productContext.SaveChangesAsync();
        return product;
    }

    public async Task<Product>  UpdateAsync(Product product)
    {
        _productContext.Update(product);
        await _productContext.SaveChangesAsync();
        return product;
    }

    public async Task<Product> RemoveAsync(Product product)
    {
        _productContext.Remove(product);
        await _productContext.SaveChangesAsync();
        return product;
    }
}