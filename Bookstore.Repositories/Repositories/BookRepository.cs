using Bookstore.Server.Data;
using Bookstore.Server.Data.Models;
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
        try
        {
            var books = await _dBContext.Books
                .Where(i => i.ItemType == "Book")
                .Include(i => i.Category)
                .ToListAsync();

            return books;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    
    public async Task<(IEnumerable<Book> item, int totalCount)> GetPaginatedAsync(int page, int perPage)
    {
        int skip = (page - 1) * perPage;
        
        try
        {
            var books = await _dBContext.Books
                .Where(i => i.ItemType == "Book")
                .Include(i => i.Category)
                .Skip(skip)
                .Take(perPage)
                .ToListAsync();
            
            var totalCount = await _dBContext.Books.CountAsync();
            
            return (books, totalCount);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
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

    public async Task<IEnumerable<Book>> GetLatestAsync(int count)
    {
        try
        {
            return await _dBContext.Books
                .OrderByDescending(b => b.Id)
                .Include(i => i.Category)
                .Take(count)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<IEnumerable<Book>> GetByCategory(int categoryId)
    {
        try
        {
            return await _dBContext.Books
                .Include(b => b.Category)
                .Where(b => b.CategoryId == categoryId)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
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