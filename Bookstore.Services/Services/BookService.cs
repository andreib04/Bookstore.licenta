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
    private readonly SortingService _sortingService;

    public BookService(IRepository<Book> bookRepository, SortingService sortingService)
    {
        _bookRepository = bookRepository;
        _sortingService = sortingService;
    }
    
    public async Task<IEnumerable<BookDTO>> GetAllAsync()
    {
        var list = await _bookRepository.GetAllAsync();
        return list.ToBookDTOList();
    }

    public async Task<(IEnumerable<BookDTO> items, int totalCount)> GetSortedPaginatedAsync(int page, int perPage,
        string sortBy, string sortOrder)
    {
        var allBooks = await _bookRepository.GetAllAsync();
        var allBooksDto = allBooks.Select(b => b.ToBookDto()).ToList();
        var sortedBooks = await _sortingService.QuickSortAsync<BookDTO>(allBooksDto, GetKeySelector(sortBy), sortOrder);

        var paginated = sortedBooks.Skip((page - 1) * perPage).Take(perPage);
        return (paginated, sortedBooks.Count());
    }

    public async Task<(IEnumerable<BookDTO> items, int totalCount)> GetSortedPaginatedByCategoryAsync(int categoryId,
        int page, int perPage,
        string sortBy, string sortOrder)
    {
        var filteredBooks = await _bookRepository.GetByCategoryAsync(categoryId);
        var filteredBooksDto = filteredBooks.Select(b => b.ToBookDto()).ToList();
        var sortedBooks = await _sortingService.QuickSortAsync<BookDTO>(filteredBooksDto, GetKeySelector(sortBy), sortOrder);
        
        var paginated = sortedBooks.Skip((page - 1) * perPage).Take(perPage);
        return (paginated, sortedBooks.Count());
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

    private Func<BookDTO, object> GetKeySelector(string sortBy)
    {
        return sortBy.ToLower() switch
        {
            "title" => book => book.Title,
            "price" => book => book.Price,
            _ => book => book.Id
        };
    }
}