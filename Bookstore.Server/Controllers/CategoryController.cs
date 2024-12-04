using Bookstore.Server.Data.Models;
using Bookstore.Server.DTOs;
using Bookstore.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Server.Controllers;

[Controller]
[Route("/api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> AddCategory([FromBody] CategoryDTO category)
    {
        await _categoryService.AddCategoryAsync(category);
        return CreatedAtAction(nameof(GetCategoryById), new {id = category.CategoryId}, category);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDTO category)
    {
        if (id != category.CategoryId)
            return BadRequest();
        
        await _categoryService.UpdateCategoryAsync(category);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        await _categoryService.DeleteCategoryAsync(id);
        return NoContent();
    }
}