using Bookstore.Server.Data.Models;
using Bookstore.Server.DTO;
using Bookstore.Server.Mappers;
using Bookstore.Server.Repositories;

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

    public async Task<BookDTO> GetByIdAsync(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        return book.toBookDTO();
    }

    public async Task AddAsync(BookDTO book)
    {
        await _bookRepository.AddAsync(book.ToBookModel());
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
        dbBook.Category = book.Category;
        
        _bookRepository.UpdateAsync(dbBook);
    }

    public async Task DeleteAsync(int id)
    {
        await _bookRepository.DeleteAsync(id);
    }
}