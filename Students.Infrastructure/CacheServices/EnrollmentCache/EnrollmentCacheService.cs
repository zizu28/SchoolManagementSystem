using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Students.Application.Contracts;
using Students.Application.DTOs.ResponseDTOs;
using Students.Domain.Entities;
using System.Text.Json;

namespace Students.Infrastructure.CacheServices.EnrollmentCache
{
	public class EnrollmentCacheService(IDistributedCache cache, 
		IEnrollmentRepository enrollmentRepository, IMapper mapper)
		: IEnrollmentCacheSevice
	{
		private readonly IDistributedCache _cache = cache;
		private readonly IEnrollmentRepository _enrollmentRepository = enrollmentRepository;
		private readonly IMapper _mapper = mapper;

		public async Task<IEnumerable<EnrollmentResponseDto>> GetAllAsync(string key, CancellationToken token = default)
		{
			var enrollments = await _cache.GetStringAsync(key, token);
			if(enrollments is null)
			{
				var enrollmentList = await _enrollmentRepository.GetAllAsync(token);
				await _cache.SetStringAsync(key, JsonSerializer.Serialize(enrollmentList), token);
				return _mapper.Map<IEnumerable<EnrollmentResponseDto>>(enrollmentList);
			}
			var result = JsonSerializer.Deserialize<IEnumerable<Enrollment>>(enrollments)!;
			return _mapper.Map<IEnumerable<EnrollmentResponseDto>>(result);
		}

		public async Task<EnrollmentResponseDto> GetAsync(Guid key, CancellationToken token = default)
		{
			var enrollment = await _cache.GetStringAsync($"enrollment-{key}", token);
			if (enrollment is null)
			{
				var enrollmentFromDb = await _enrollmentRepository.GetByIdAsync(key, token);
				await _cache.SetStringAsync($"enrollment-{key}", JsonSerializer.Serialize(enrollmentFromDb), token);
				return _mapper.Map<EnrollmentResponseDto>(enrollmentFromDb);
			}
			var result = JsonSerializer.Deserialize<Enrollment>(enrollment)!;
			return _mapper.Map<EnrollmentResponseDto>(result);
		}

		public async Task RemoveAsync(Guid key, CancellationToken token = default)
		{
			await _cache.RemoveAsync($"enrollment-{key}", token);
		}

		public async Task SetAsync(Guid key, Enrollment value, CancellationToken token = default)
		{
			await _cache.SetStringAsync($"enrollment-{key}", JsonSerializer.Serialize(value), new DistributedCacheEntryOptions
			{
				AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
				SlidingExpiration = TimeSpan.FromMinutes(2)
			}, token);
		}

		public async Task SetManyAsync(string key, IEnumerable<Enrollment> values, CancellationToken token = default)
		{
			await _cache.SetStringAsync(key, JsonSerializer.Serialize(values), new DistributedCacheEntryOptions
			{
				AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
				SlidingExpiration = TimeSpan.FromMinutes(2)
			}, token);
		}
	}
}
