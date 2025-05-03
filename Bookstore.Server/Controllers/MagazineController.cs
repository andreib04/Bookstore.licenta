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
    private readonly SortingService _sortingService;

    public MagazineController(IService<MagazineDTO> magazineService, SortingService sortingService)
    {
        _magazineService = magazineService;
        _sortingService = sortingService;
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
    
    [HttpGet("sorted")]
    public async Task<IActionResult> GetSortedProducts(string sortBy = "price", string sortOrder = "asc")
    {
        var magazines = await _magazineService.GetAllAsync();

        IEnumerable<MagazineDTO> sortedBooks;

        if (sortBy == "price")
        {
            sortedBooks = await _sortingService.QuickSortAsync<MagazineDTO>(magazines, m => m.Price, sortOrder);
        }
        else if (sortBy == "name")
        {
            sortedBooks = await _sortingService.QuickSortAsync<MagazineDTO>(magazines, m => m.Title, sortOrder);
        }
        else
        {
            sortedBooks = magazines;
        }
        
        return Ok(sortedBooks);
    }

    [HttpGet("paginated")]
    [AllowAnonymous]
    public async Task<IActionResult> GetPaginatedMagazines(int page = 1, int perPage = 20)
    {
        try
        {
            var (magazines, totalCount) = await _magazineService.GetPaginatedAsync(page, perPage);
            return Ok(new { magazines, totalCount });
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
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