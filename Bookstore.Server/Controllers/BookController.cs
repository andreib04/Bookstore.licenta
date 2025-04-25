using Bookstore.Server.Data.Models;
using Bookstore.Server.DTO;
using Bookstore.Server.Services;
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

    [HttpPost]
    /*[Authorize(Policy = "Admin")]*/
    [AllowAnonymous]
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
    [Authorize(Policy = "Admin")]
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
    [Authorize(Policy = "Admin")]
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