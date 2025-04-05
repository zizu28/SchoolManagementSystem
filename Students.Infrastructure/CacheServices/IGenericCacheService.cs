namespace Students.Infrastructure.CacheServices
{
	public interface IGenericCacheService<T1, T2> where T1 : class where T2 : class
	{
		Task<IEnumerable<T2>> GetAllAsync(string key, CancellationToken token = default);
		Task<T2> GetAsync(Guid key, CancellationToken token = default);
		Task SetAsync(Guid key, T1 value, CancellationToken token = default);
		Task SetManyAsync(string key, IEnumerable<T1> values, CancellationToken token = default);
		Task RemoveAsync(Guid key, CancellationToken token = default);
	}
}
