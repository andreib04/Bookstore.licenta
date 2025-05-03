using Bookstore.Server.Data.Models;

namespace Bookstore.Server.Repositories
{
	public interface IRepository<T> where T : class
	{
		Task<IEnumerable<T>> GetAllAsync();
		Task<(IEnumerable<T> item, int totalCount)> GetPaginatedAsync(int page, int perPage);
		Task<T> GetByIdAsync(int id);
		Task<IEnumerable<T>> GetLatestAsync(int count);
		Task<T> AddAsync(T entity);
		Task UpdateAsync(T entity);
		Task DeleteAsync(int id);
	}
}
