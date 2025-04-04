using Microsoft.Extensions.Caching.Hybrid;
using Students.Application.Contracts;
using Students.Domain.Entities;

namespace Students.Infrastructure.CacheServices.AdmissionApplicationCache
{
	public class AdmissionApplicationCacheService(HybridCache cache, IAdmissionApplicationRepository repository) 
		: IAdmissionApplicationCache
	{
		private readonly HybridCache _cache = cache;
		private readonly IAdmissionApplicationRepository _repository = repository;

		public async Task<IEnumerable<AdmissionApplication>> GetAllAsync(string key, CancellationToken token = default)
		{
			return await _cache.GetOrCreateAsync(key, async entry =>
			{
				return await _repository.GetAllAsync(entry);
			}, cancellationToken: token);
		}

		public async Task<AdmissionApplication> GetAsync(Guid key, CancellationToken token = default)
		{
			return await _cache.GetOrCreateAsync($"admission-application-{key}", async entry =>
			{
				return await _repository.GetByIdAsync(key, entry);
			}, cancellationToken: token);
		}

		public async Task RemoveAsync(Guid key, CancellationToken token = default)
		{
			await _cache.RemoveAsync($"admission-application-{key}", cancellationToken: token);
		}

		public async Task SetAsync(Guid key, AdmissionApplication value, CancellationToken token = default)
		{
			await _cache.SetAsync($"admission-application-{key}", value, cancellationToken: token);
		}

		public async Task SetManyAsync(string key, IEnumerable<AdmissionApplication> values, CancellationToken token = default)
		{
			await _cache.SetAsync(key, values, cancellationToken: token);
		}
	}
}
