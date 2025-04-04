using Microsoft.Extensions.Caching.Hybrid;
using Students.Application.Contracts;
using Students.Domain.Entities;

namespace Students.Infrastructure.CacheServices.AcademicRecordCache
{
	public class AcademicRecordCacheService(HybridCache cache, IAcademicRecordRepository repository) 
		: IAcademicRecordCacheService
	{
		private readonly HybridCache _cache = cache;
		private readonly IAcademicRecordRepository _repository = repository;

		public async Task<IEnumerable<AcademicRecord>> GetAllAsync(string key, CancellationToken token = default)
		{
			var academicRecords = await _cache.GetOrCreateAsync(key, async entry =>
			{
				return await _repository.GetAllAsync(token);
			}, cancellationToken: token);
			return academicRecords;
		}

		public async Task<AcademicRecord> GetAsync(Guid key, CancellationToken token = default)
		{
			var academicRecord = await _cache.GetOrCreateAsync($"academic-record-{key}", async entry =>
			{
				return await _repository.GetByIdAsync(key, token);
			}, cancellationToken: token);
			return academicRecord;
		}

		public async Task RemoveAsync(Guid key, CancellationToken token = default)
		{
			await _cache.RemoveAsync($"academic-record-{key}", cancellationToken: token);
		}

		public async Task SetAsync(Guid key, AcademicRecord value, CancellationToken token = default)
		{
			await _cache.SetAsync($"academic-record-{key}", value, cancellationToken: token);
		}

		public async Task SetManyAsync(string key, IEnumerable<AcademicRecord> values, CancellationToken token = default)
		{
			await _cache.SetAsync(key, values, cancellationToken: token);
		}
	}
}
