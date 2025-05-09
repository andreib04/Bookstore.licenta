namespace Bookstore.Server.Services;

public interface IService<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task<(IEnumerable<T> items, int totalCount)> GetSortedPaginatedAsync(int page, int perPage, string sortBy, string sortOrder);
    Task<(IEnumerable<T> items, int totalCount)> GetSortedPaginatedByCategoryAsync(int categoryId, int page, int perPage, string sortBy, string sortOrder);
    Task<IEnumerable<T>> GetLatestAsync(int count);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
}