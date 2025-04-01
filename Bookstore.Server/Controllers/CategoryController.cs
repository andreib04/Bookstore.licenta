using Bookstore.Server.Data.Models;
using Bookstore.Server.DTOs;
using Bookstore.Server.Services;
using Microsoft.AspNetCore.Authorization;
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
    [AllowAnonymous]
    public async Task<IActionResult> GetAllCategories()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        return Ok(categories);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        return Ok(category);
    }

    [HttpPost]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> AddCategory([FromBody] CategoryDTO category)
    {
        await _categoryService.AddCategoryAsync(category);
        return CreatedAtAction(nameof(GetCategoryById), new {id = category.CategoryId}, category);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDTO category)
    {
        if (id != category.CategoryId)
            return BadRequest();
        
        var updatedCategory = await _categoryService.UpdateCategoryAsync(category);

        return Ok(updatedCategory);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        await _categoryService.DeleteCategoryAsync(id);
        return NoContent();
    }
}