using Bookstore.Server.Data.Models;
using Bookstore.Server.DTOs;
using Bookstore.Server.Mappers;
using Bookstore.Server.Repositories;

namespace Bookstore.Server.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    
    public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync()
    {
        var list = await _categoryRepository.GetAllCategoriesAsync();
        return list.ToCategoryDTOList();
    }

    public async Task<CategoryDTO> GetCategoryByIdAsync(int id)
    {
        var category = await _categoryRepository.GetCategoryByIdAsync(id);
        return category.ToDTO();
    }

    public async Task AddCategoryAsync(CategoryDTO category)
    {
        await _categoryRepository.AddCategoryAsync(category.ToModel());
    }

    public async Task UpdateCategoryAsync(CategoryDTO category)
    {
        var dbCategory = await _categoryRepository.GetCategoryByIdAsync(category.CategoryId);
        
        dbCategory.CategoryId = category.CategoryId;
        dbCategory.CategoryName = category.CategoryName;

        _categoryRepository.UpdateCategoryAsync(dbCategory);
    }

    public async Task DeleteCategoryAsync(int id)
    {
        await _categoryRepository.DeleteCategoryAsync(id);
    }
}