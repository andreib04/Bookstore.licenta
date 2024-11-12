using Bookstore.Server.Data.Models;
using Bookstore.Server.Repositories;

namespace Bookstore.Server.Services;

public class BookService : IService<Book>
{
    private readonly IRepository<Book> _bookRepository;

    public BookService(IRepository<Book> bookRepository)
    {
        _bookRepository = bookRepository;
    }
    
    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        return await _bookRepository.GetAllAsync();
    }

    public async Task<Book> GetByIdAsync(int id)
    {
        return await _bookRepository.GetByIdAsync(id);
    }

    public async Task AddAsync(Book book)
    {
        await _bookRepository.AddAsync(book);
    }

    public async Task UpdateAsync(Book book)
    {
        var dbBook = await _bookRepository.GetByIdAsync(book.Id);
        
        dbBook.Title = book.Title;
        dbBook.Author = book.Author;
        dbBook.Description = book.Description; 
        dbBook.Price = book.Price;
        dbBook.Stock = book.Stock;
        dbBook.Image = book.Image;
        dbBook.Category = book.Category;
        
        _bookRepository.UpdateAsync(dbBook);
    }

    public async Task DeleteAsync(int id)
    {
        await _bookRepository.DeleteAsync(id);
    }
}