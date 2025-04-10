﻿namespace Courses.Application.Contracts
{
	public interface IGenericRepository<T>
	{
		Task<T> GetByIdAsync(int id);
		Task<IEnumerable<T>> GetAllAsync();
		Task AddAsync(T entity);
		Task UpdateAsync(T entity);
		Task DeleteAsync(int id);
	}
}
