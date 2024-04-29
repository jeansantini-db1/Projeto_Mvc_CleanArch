using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchMvc.Domain.Entities;

namespace CleanArchMvc.Domain.Interfaces;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetCategoriesAsync();
    Task<Category> GetByIdAsync(int? id);
    
    Task CreateAsync(Category category);
    Task UpdateAsync(Category category);
    Task RemoveAsync(Category category);
}