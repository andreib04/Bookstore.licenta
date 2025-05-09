using Bookstore.Server.Data.Models;
using Bookstore.Server.DTOs;
using Bookstore.Server.Services;
using Bookstore.Services.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Server.Controllers;

[Controller]
[Route("/api/[controller]")]
public class MagazineController : ControllerBase
{
    private readonly IService<MagazineDTO> _magazineService;

    public MagazineController(IService<MagazineDTO> magazineService)
    {
        _magazineService = magazineService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllMagazines()
    {
        try
        {
            var magazine = await _magazineService.GetAllAsync();
            return Ok(magazine);
        }
        catch
        {
            return new ObjectResult("Something went wrong")
            {
                StatusCode = 500
            };
        }
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetMagazineById(int id)
    {
        try
        {
            var magazine = await _magazineService.GetByIdAsync(id);
            return Ok(magazine);
        }
        catch
        {
            return new ObjectResult("Something went wrong")
            {
                StatusCode = 500
            };
        }
    }

    [HttpGet("latest/{count}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetLatestMagazines(int count)
    {
        try
        {
            var magazines = await _magazineService.GetLatestAsync(count);
            return Ok(magazines);
        }
        catch(Exception ex)
        {
            throw new Exception($"Something went wrong {ex.Message}");
        }
    }

    [HttpGet("sorted-paginated")]
    [AllowAnonymous]
    public async Task<IActionResult> GetSortedPaginated(
        [FromQuery] int page = 1,
        [FromQuery] int perPage = 20,
        [FromQuery] string sortBy = "price",
        [FromQuery] string sortOrder = "desc")
    {
        try
        {
            var (magazines, totalCount) = await _magazineService.GetSortedPaginatedAsync(page, perPage, sortBy, sortOrder);
            return Ok(new { items = magazines, totalCount });
        }
        catch (Exception ex)
        {
            throw new Exception($"Something went wrong: {ex.Message}");
        }
    }

    [HttpGet("by-category/{categoryId}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetSortedPaginatedByCategory(
        int categoryId,
        [FromQuery] int page = 1,
        [FromQuery] int perPage = 20,
        [FromQuery] string sortBy = "price",
        [FromQuery] string sortOrder = "desc")
    {
        try
        {
            var (magazines, totalCount) = await _magazineService.GetSortedPaginatedByCategoryAsync(categoryId, page, perPage, sortBy, sortOrder);
            return Ok(new { items = magazines, totalCount });
        }
        catch (Exception ex)
        {
            throw new Exception($"Something went wrong: {ex.Message}");
        }
    }

    [HttpPost]
    [Authorize(Policy = UserRolesConstants.Admin)]
    public async Task<IActionResult> AddMagazine([FromBody] MagazineDTO magazine)
    {
        try
        { 
            await _magazineService.AddAsync(magazine);
            return Ok(magazine);
        }
        catch (Exception e)
        {
            return new NotFoundObjectResult(e.Message);
        }
    }

    [HttpPut("{id}")]
    [Authorize(Policy = UserRolesConstants.Admin)]
    public async Task<IActionResult> UpdateMagazine(int id, [FromBody] MagazineDTO magazine)
    {
        if (id != magazine.Id)
            return BadRequest();

        try
        {
            await _magazineService.UpdateAsync(magazine);
            return NoContent();
        }
        catch (Exception e)
        {
            return new NotFoundObjectResult(e.Message);
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = UserRolesConstants.Admin)]
    public async Task<IActionResult> DeleteMagazine(int id)
    {
        try
        {
            await _magazineService.DeleteAsync(id);
            return NoContent();
        }
        catch
        {
            return new ObjectResult("Something went wrong")
            {
                StatusCode = 500
            };
        }
    }
}