using Bookstore.Server.Data.Models;

namespace Bookstore.Server.Repositories;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllCategoriesAsync();
    Task<Category> GetCategoryByIdAsync(int id);
    Task AddCategoryAsync(Category category);
    void UpdateCategoryAsync(Category category);
    Task DeleteCategoryAsync(int id);
}