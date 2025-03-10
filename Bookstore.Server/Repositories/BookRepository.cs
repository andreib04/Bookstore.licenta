using Bookstore.Server.Data;
using Bookstore.Server.Data.Models;
using Bookstore.Server.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Server.Repositories;

public class BookRepository : IRepository<Book>
{
    private readonly DatabaseContext _dBContext;

    public BookRepository(DatabaseContext dBContext)
    {
        _dBContext = dBContext;
    }
    
    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        return await _dBContext.Books
            .Where(i => i.ItemType == "Book")
            .Include(i => i.Category)
            .ToListAsync();
    }

    public async Task<Book> GetByIdAsync(int id)
    {
        var book = await _dBContext.Books
            .Include(i => i.Category)
            .FirstOrDefaultAsync(i => i.ItemType == "Book" && i.Id == id);

        if (book == null)
        {
            throw new Exception($"Book with id {id} not found");
        }
        
        return book; 
    }

    public async Task<Book> AddAsync(Book book)
    {
        book.ItemType = "Book";
        _dBContext.Books.Add(book);
        await _dBContext.SaveChangesAsync();
        return book;
    }

    public async Task UpdateAsync(Book book)
    {
        _dBContext.Books.Update(book);
        await _dBContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var book = await GetByIdAsync(id);
      
        _dBContext.Books.Remove(book);
        await _dBContext.SaveChangesAsync();

    }
}