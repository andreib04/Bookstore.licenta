using System.Collections;
using Bookstore.Server.Data.Models;
using Bookstore.Server.DTO;
using Bookstore.Server.Mappers;
using Bookstore.Server.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Bookstore.Server.Services;

public class BookService : IService<BookDTO>
{
    private readonly IRepository<Book> _bookRepository;

    public BookService(IRepository<Book> bookRepository)
    {
        _bookRepository = bookRepository;
    }
    
    public async Task<IEnumerable<BookDTO>> GetAllAsync()
    {
        var list = await _bookRepository.GetAllAsync();
        return list.ToBookDTOList();
    }

    public async Task<(IEnumerable<BookDTO> item, int totalCount)> GetPaginatedAsync(int page, int perPage)
    {
        var (books, totalCount) = await _bookRepository.GetPaginatedAsync(page, perPage);
        
        var booksDto = books.Select(b => b.ToBookDto()).ToList();
        
        return (booksDto, totalCount);
    }

    public async Task<BookDTO> GetByIdAsync(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        return book.ToBookDto();
    }

    public async Task<IEnumerable<BookDTO>> GetLatestAsync(int count)
    {
        var books = await _bookRepository.GetLatestAsync(count);
        return books.ToBookDTOList();
    }

    public async Task<IEnumerable<BookDTO>> GetByCategory(int categoryId)
    {
        var books = await _bookRepository.GetByCategory(categoryId);
        return books.ToBookDTOList();
    }
    
    public async Task<BookDTO> AddAsync(BookDTO book)
    {
        await _bookRepository.AddAsync(book.ToBookModel());
        return book;
    }

    public async Task UpdateAsync(BookDTO book)
    {
        var dbBook = await _bookRepository.GetByIdAsync(book.Id);
        
        dbBook.Title = book.Title;
        dbBook.Author = book.Author;
        dbBook.Description = book.Description; 
        dbBook.Price = book.Price;
        dbBook.Stock = book.Stock;
        dbBook.Image = book.Image;
        
        await _bookRepository.UpdateAsync(dbBook);
    }

    public async Task DeleteAsync(int id)
    {
        await _bookRepository.DeleteAsync(id);
    }
}