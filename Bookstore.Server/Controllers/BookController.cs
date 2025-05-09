using Bookstore.Server.DTO;
using Bookstore.Server.Services;
using Bookstore.Services.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Server.Controllers;

[Controller]
[Route("/api/[controller]")]
public class BookController : ControllerBase
{
    private readonly IService<BookDTO> _service;

    public BookController(IService<BookDTO> service)
    {
        _service = service;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllBooks()
    {
        try
        {
            var books = await _service.GetAllAsync();
            return Ok(books);
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
    public async Task<IActionResult> GetBookById(int id)
    {
        try
        {
            var book = await _service.GetByIdAsync(id);
            return Ok(book);
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
    public async Task<IActionResult> GetLatestBooks(int count)
    {
        try
        {
            var books = await _service.GetLatestAsync(count);
            return Ok(books);
        }
        catch (Exception ex)
        {
            throw new Exception($"Something went wrong: {ex.Message}");
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
            var (books, totalCount) = await _service.GetSortedPaginatedAsync(page, perPage, sortBy, sortOrder);
            return Ok(new
            {
                items = books, 
                totalCount
            });
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
            var (books, totalCount) = await _service.GetSortedPaginatedByCategoryAsync(categoryId, page, perPage, sortBy, sortOrder);
            return Ok(new { items = books, totalCount });
        }
        catch (Exception ex)
        {
            throw new Exception($"Something went wrong: {ex.Message}");
        }
    }

    [HttpPost]
    [Authorize(Policy = UserRolesConstants.Admin)]
    public async Task<IActionResult> AddBook([FromBody]BookDTO book)
    {
        try
        {
            await _service.AddAsync(book);
            return Ok(book);
        }
        catch(KeyNotFoundException e)
        {
            return new NotFoundObjectResult(e.Message);
        }
    }

    [HttpPut("{id}")]
    [Authorize(Policy = UserRolesConstants.Admin)]
    public async Task<IActionResult> UpdateBook(int id,[FromBody] BookDTO book)
    {
        if(id != book.Id)
            return BadRequest();

        try
        {
            await _service.UpdateAsync(book);
            return NoContent();
        }
        catch (KeyNotFoundException e)
        {
            return new NotFoundObjectResult(e.Message);
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = UserRolesConstants.Admin)]
    public async Task<IActionResult> DeleteBook(int id)
    {
        try
        {
            await _service.DeleteAsync(id);
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