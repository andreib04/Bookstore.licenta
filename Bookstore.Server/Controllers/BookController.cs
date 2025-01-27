using Bookstore.Server.Data.Models;
using Bookstore.Server.DTO;
using Bookstore.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Server.Controllers;

[Controller]
[Route("/api/[controller]")]
public class BookController : ControllerBase
{
    private readonly IBookService _service;

    public BookController(IBookService service)
    {
        _service = service;
    }

    [HttpGet]
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

    [HttpPost]
    public async Task<IActionResult> AddBook([FromBody]BookDTO book)
    {
        try
        {
            await _service.AddAsync(book);
            return Ok();
        }
        catch(KeyNotFoundException e)
        {
            return new NotFoundObjectResult(e.Message);
        }
    }

    [HttpPut("{id}")]
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