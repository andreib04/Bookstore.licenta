using System.Collections;
using Bookstore.Server.Data.Models;
using Bookstore.Server.DTO;

namespace Bookstore.Server.Services;

public interface IBookService
{
    Task<IEnumerable> GetAllAsync();
    Task<BookDTO> GetByIdAsync(int id);
    Task<BookDTO> AddAsync(BookDTO entity);
    Task UpdateAsync(BookDTO entity);
    Task DeleteAsync(int id);
}