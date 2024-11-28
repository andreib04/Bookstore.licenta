using Bookstore.Server.Data.Models;
using Bookstore.Server.DTOs;

namespace Bookstore.Server.Services;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync();
    Task<CategoryDTO> GetCategoryByIdAsync(int id);
    Task AddCategoryAsync(CategoryDTO category);
    Task UpdateCategoryAsync(CategoryDTO category);
    Task DeleteCategoryAsync(int id);
}