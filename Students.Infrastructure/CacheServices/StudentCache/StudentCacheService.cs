using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Hybrid;
using Students.Application.Contracts;
using Students.Application.DTOs.ResponseDTOs;
using Students.Domain.Entities;
using System.Text.Json;

namespace Students.Infrastructure.CacheServices.StudentCache
{
	public class StudentCacheService(IDistributedCache cache, IMapper mapper) 
		: IStudentCacheService
	{
		private readonly IDistributedCache _cache = cache;
		private readonly IMapper _mapper = mapper;

		public async Task<IEnumerable<StudentResponseDto>> GetAllAsync(string key, CancellationToken token = default)
		{
			var students = await _cache.GetStringAsync(key, token);
			if (students != null)
			{
				var result = JsonSerializer.Deserialize<IEnumerable<Student>>(students);
				return _mapper.Map<IEnumerable<StudentResponseDto>>(result);
			}
			return [];
		}

		public async Task<StudentResponseDto> GetAsync(Guid key, CancellationToken token = default)
		{
			var student = await _cache.GetStringAsync($"student-{key}", token);
			if (student != null)
			{
				var result = JsonSerializer.Deserialize<Student>(student);
				return _mapper.Map<StudentResponseDto>(result);
			}
			return default!;
		}

		public async Task RemoveAsync(Guid key, CancellationToken token = default)
		{
			await _cache.RemoveAsync($"student-{key}", token);
		}

		public async Task SetAsync(Guid key, Student value, CancellationToken token = default)
		{
			await _cache.SetStringAsync($"student-{key}", JsonSerializer.Serialize(value), new DistributedCacheEntryOptions
			{
				AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
				SlidingExpiration = TimeSpan.FromMinutes(2)
			}, token);
		}

		public async Task SetManyAsync(string key, IEnumerable<Student> values, CancellationToken token = default)
		{
			await _cache.SetStringAsync(key, JsonSerializer.Serialize(values), new DistributedCacheEntryOptions
			{
				AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
				SlidingExpiration = TimeSpan.FromMinutes(2)
			}, token);
		}
	}
}
