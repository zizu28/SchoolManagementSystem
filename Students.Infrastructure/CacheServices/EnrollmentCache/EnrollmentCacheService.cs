using Microsoft.Extensions.Caching.Hybrid;
using Students.Application.Contracts;
using Students.Domain.Entities;

namespace Students.Infrastructure.CacheServices.EnrollmentCache
{
	public class EnrollmentCacheService(HybridCache cache, IEnrollmentRepository enrollmentRepository)
		: IEnrollmentCacheSevice
	{
		private readonly HybridCache _cache = cache;
		private readonly IEnrollmentRepository _enrollmentRepository = enrollmentRepository;

		public async Task<IEnumerable<Enrollment>> GetAllAsync(string key, CancellationToken token = default)
		{
			var enrollments = await _cache.GetOrCreateAsync(key, async entry =>
			{
				return await _enrollmentRepository.GetAllAsync(token);
			}, cancellationToken: token);
			return enrollments;
		}

		public async Task<Enrollment> GetAsync(Guid key, CancellationToken token = default)
		{
			var enrollment = await _cache.GetOrCreateAsync($"enrollment-{key}", async entry =>
			{
				return await _enrollmentRepository.GetByIdAsync(key, token);
			}, cancellationToken: token);
			return enrollment;
		}

		public async Task RemoveAsync(Guid key, CancellationToken token = default)
		{
			await _cache.RemoveAsync($"enrollment-{key}", token);
		}

		public async Task SetAsync(Guid key, Enrollment value, CancellationToken token = default)
		{
			await _cache.SetAsync($"enrollment-{key}", value, cancellationToken: token);
		}

		public async Task SetManyAsync(string key, IEnumerable<Enrollment> values, CancellationToken token = default)
		{
			await _cache.SetAsync(key, values, cancellationToken: token);
		}
	}
}
