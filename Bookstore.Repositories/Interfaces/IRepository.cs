﻿using Bookstore.Server.Data.Models;

namespace Bookstore.Server.Repositories
{
	public interface IRepository<T> where T : class
	{
		Task<IEnumerable<T>> GetAllAsync();
		Task<T> GetByIdAsync(int id);
		Task<IEnumerable<T>> GetLatestAsync(int count);
		Task<IEnumerable<T>> GetByCategoryAsync(int categoryId);
		SortedList<string, List<T>> GetSortedByName();
		Task<T> AddAsync(T entity);
		Task UpdateAsync(T entity);
		Task DeleteAsync(int id);
		Task UpdateStockAsync(int productId, int quantity);
	}
}
