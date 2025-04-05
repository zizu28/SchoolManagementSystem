namespace Students.Application.Contracts
{
	public interface IGenericRepository<T>  
	{
		Task<IEnumerable<T>> GetAllAsync(CancellationToken token = default);
		Task<T> GetByIdAsync(Guid Id, CancellationToken token = default);
		Task AddAsync(T entity, CancellationToken token = default);
		Task UpdateAsync(T entity, CancellationToken token = default);
		Task DeleteAsync(T entity, CancellationToken token = default);
	}
}
