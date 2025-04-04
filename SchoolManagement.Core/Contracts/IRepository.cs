using SchoolManagement.Core.Entities;

namespace SchoolManagement.Core.Interfaces
{
	public interface IRepository<T> where T : EntityBase
	{
		Task<IEnumerable<T>> GetAllAsync();
		Task<T> GetByIdAsync(Guid id);
		Task AddAsync(T entity);
		Task UpdateAsync(T entity);
		Task DeleteAsync(T entity);
	}
}
