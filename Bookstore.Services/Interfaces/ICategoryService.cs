using Bookstore.Server.Data.Models;
using Bookstore.Server.DTOs;

namespace Bookstore.Server.Services;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync();
    Task<(IEnumerable<CategoryDTO> items, int totalCount)> GetPaginatedCategoriesAsync(int page, int perPage);
    Task<CategoryDTO> GetCategoryByIdAsync(int id);
    Task AddCategoryAsync(CategoryDTO category);
    Task<Category> UpdateCategoryAsync(CategoryDTO category);
    Task DeleteCategoryAsync(int id);
}