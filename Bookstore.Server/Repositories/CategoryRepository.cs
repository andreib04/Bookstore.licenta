using Bookstore.Server.Data;
using Bookstore.Server.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Server.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly DatabaseContext _dbContext;

    public CategoryRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
    {
        return await _dbContext.Categories.ToListAsync();
    }

    public async Task<Category> GetCategoryByIdAsync(int id)
    {
        var category = await _dbContext.Categories
            .FirstOrDefaultAsync(i => i.CategoryId == id);
        
        if(category == null)
            throw new Exception($"Category with id {id} not found");
        
        return category;
    }
    
    public async Task AddCategoryAsync(Category category)
    {
        _dbContext.Categories.Add(category);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateCategoryAsync(Category category)
    {
        _dbContext.Categories.Update(category);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteCategoryAsync(int id)
    {
        var category = await _dbContext.Categories.FindAsync(id);
        
        if (category != null) 
            _dbContext.Categories.Remove(category);
        
        await _dbContext.SaveChangesAsync();
    }
}