using System.Collections;
using Bookstore.Server.Data.Models;

namespace Bookstore.Server.Services;

public interface IBookService
{
    Task<IEnumerable> GetAllAsync();
    Task GetByIdAsync(int id);
    Task AddAsync(Book entity);
    Task UpdateAsync(Book entity);
    Task DeleteAsync(int id);
}