using Bookstore.Server.Data.Models;
using Bookstore.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Server.Controllers;

[Controller]
[Route("/api/[controller]")]
public class MagazineController : ControllerBase
{
    private readonly IService<Magazine> _magazineService;

    public MagazineController(IService<Magazine> magazineService)
    {
        _magazineService = magazineService;
    }

    [HttpGet]
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

    [HttpPost]
    public async Task<IActionResult> AddMagazine([FromBody] Magazine magazine)
    {
        try
        {
            await _magazineService.AddAsync(magazine);
            return CreatedAtAction(nameof(GetMagazineById), new { id = magazine.Id }, magazine);
        }
        catch (KeyNotFoundException e)
        {
            return new NotFoundObjectResult(e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMagazine(int id, [FromBody] Magazine magazine)
    {
        if (id != magazine.Id)
            return BadRequest();

        try
        {
            await _magazineService.UpdateAsync(magazine);
            return NoContent();
        }
        catch (KeyNotFoundException e)
        {
            return new NotFoundObjectResult(e.Message);
        }
    }

    [HttpDelete("{id}")]
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