﻿using Bookstore.Server.Data.Models;

namespace Bookstore.Server.Repositories
{
	public interface IRepository<T> where T : class
	{
		Task<IEnumerable<T>> GetAllAsync();	
		Task<T> GetByIdAsync(int id);
		Task AddAsync(T entity);
		void UpdateAsync(T entity);
		Task DeleteAsync(int id);
	}
}