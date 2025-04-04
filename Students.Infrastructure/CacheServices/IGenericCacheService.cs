namespace Students.Infrastructure.CacheServices
{
	public interface IGenericCacheService<T>
	{
		Task<IEnumerable<T>> GetAllAsync(string key, CancellationToken token = default);
		Task<T> GetAsync(Guid key, CancellationToken token = default);
		Task SetAsync(Guid key, T value, CancellationToken token = default);
		Task SetManyAsync(string key, IEnumerable<T> values, CancellationToken token = default);
		Task RemoveAsync(Guid key, CancellationToken token = default);
	}
}
