using System.Text.RegularExpressions;
using Bookstore.Server.Data.Models;
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
    private readonly SortingService _sortingService;

    public BookController(IService<BookDTO> service, SortingService sortingService)
    {
        _service = service;
        _sortingService = sortingService;
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

    [HttpGet("{id}/image")]
    public async Task<IActionResult> GetBookImage(int id)
    {
        var book = await _service.GetByIdAsync(id);

        if (book == null || string.IsNullOrEmpty(book.Image))
        {
            return NotFound("Image not found");
        }
        
        var base64String = book.Image;
        
        var dataUriPrefix = "data:image/jpeg;base64,";
        if (base64String.StartsWith(dataUriPrefix))
        {
            base64String = base64String.Substring(dataUriPrefix.Length);
        }
        
        base64String = Regex.Replace(base64String, @"[^A-Za-z0-9\+/=]", "");

        try
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            
            return File(imageBytes, "image/jpeg");  
        }
        catch (FormatException ex)
        {
            return BadRequest("Invalid base64 string: " + ex.Message);
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

    [HttpGet("sorted")]
    [AllowAnonymous]
    public async Task<IActionResult> GetSortedProducts(string sortBy = "price", string sortOrder = "asc")
    {
        var books = await _service.GetAllAsync();

        IEnumerable<BookDTO> sortedBooks;

        if (sortBy == "price")
        {
            sortedBooks = await _sortingService.QuickSortAsync<BookDTO>(books, b => b.Price, sortOrder);
        }
        else if (sortBy == "name")
        {
            sortedBooks = await _sortingService.QuickSortAsync<BookDTO>(books, b => b.Title, sortOrder);
        }
        else
        {
            sortedBooks = books;
        }
        
        return Ok(sortedBooks);
    }

    [HttpGet("paginated")]
    [AllowAnonymous]
    public async Task<IActionResult> GetPaginatedBooks(int page = 1, int perPage = 20)
    {
        try
        {
            var (books, totalCount) = await _service.GetPaginatedAsync(page, perPage);
            return Ok(new { books, totalCount });
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
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