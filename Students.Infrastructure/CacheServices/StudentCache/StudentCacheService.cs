using Microsoft.Extensions.Caching.Hybrid;
using Students.Application.Contracts;
using Students.Domain.Entities;

namespace Students.Infrastructure.CacheServices.StudentCache
{
	public class StudentCacheService(HybridCache cache, IStudentRepository repository) 
		: IStudentCacheService
	{
		private readonly HybridCache _cache = cache;
		private readonly IStudentRepository _repository = repository;

		public async Task<IEnumerable<Student>> GetAllAsync(string key, CancellationToken token = default)
		{
			var students = await _cache.GetOrCreateAsync(key, async entry =>
			{
				return await _repository.GetAllAsync(entry);
			}, cancellationToken: token);
			return students;
		}

		public async Task<Student> GetAsync(Guid key, CancellationToken token = default)
		{
			var student = await _cache.GetOrCreateAsync($"student-{key}", async entry =>
			{
				return await _repository.GetByIdAsync(key, entry);
			}, cancellationToken: token);
			return student;
		}

		public async Task RemoveAsync(Guid key, CancellationToken token = default)
		{
			await _cache.RemoveAsync($"student-{key}", token);
		}

		public async Task SetAsync(Guid key, Student value, CancellationToken token = default)
		{
			await _cache.SetAsync($"student-{key}", value, cancellationToken: token);
		}

		public async Task SetManyAsync(string key, IEnumerable<Student> values, CancellationToken token = default)
		{
			await _cache.SetAsync(key, values, cancellationToken: token);
		}
	}
}
