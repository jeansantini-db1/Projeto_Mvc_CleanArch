using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchMvc.Application.DTOs;

namespace CleanArchMvc.Application.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
    Task<CategoryDto> GetByIdAsync(int? id);
    
    Task CreateAsync(CategoryDto categoryDto);
    Task UpdateAsync(CategoryDto categoryDto);
    Task RemoveAsync(int? id);
}