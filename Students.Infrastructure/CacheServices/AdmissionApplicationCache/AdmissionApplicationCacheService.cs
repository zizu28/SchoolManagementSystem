using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Hybrid;
using Students.Application.Contracts;
using Students.Application.DTOs.ResponseDTOs;
using Students.Domain.Entities;
using System.Text.Json;

namespace Students.Infrastructure.CacheServices.AdmissionApplicationCache
{
	public class AdmissionApplicationCacheService(IDistributedCache cache, 
		IAdmissionApplicationRepository repository, IMapper mapper) 
		: IAdmissionApplicationCache
	{
		private readonly IDistributedCache _cache = cache;
		private readonly IAdmissionApplicationRepository _repository = repository;
		private readonly IMapper _mapper = mapper;

		public async Task<IEnumerable<AdmissionApplicationResponseDto>> GetAllAsync(string key, CancellationToken token = default)
		{
			var admissionApplications = await _cache.GetStringAsync(key, token);
			if(admissionApplications is null)
			{
				var admissionApplicationsFromDb = await _repository.GetAllAsync(token);
				await _cache.SetStringAsync(key, JsonSerializer.Serialize(admissionApplicationsFromDb), token);
				return _mapper.Map<IEnumerable<AdmissionApplicationResponseDto>>(admissionApplicationsFromDb);
			}
			var result = JsonSerializer.Deserialize<IEnumerable<AdmissionApplication>>(admissionApplications)!;
			return _mapper.Map<IEnumerable<AdmissionApplicationResponseDto>>(result);
		}

		public async Task<AdmissionApplicationResponseDto> GetAsync(Guid key, CancellationToken token = default)
		{
			var admissionApplication = await _cache.GetStringAsync($"admission-application-{key}", token);
			if (admissionApplication is null)
			{
				var admissionApplicationFromDb = await _repository.GetByIdAsync(key, token);
				await _cache.SetStringAsync($"admission-application-{key}", JsonSerializer.Serialize(admissionApplicationFromDb), token);
				return _mapper.Map<AdmissionApplicationResponseDto>(admissionApplicationFromDb);
			}
			var result = JsonSerializer.Deserialize<AdmissionApplication>(admissionApplication)!;
			return _mapper.Map<AdmissionApplicationResponseDto>(result);
		}

		public async Task RemoveAsync(Guid key, CancellationToken token = default)
		{
			await _cache.RemoveAsync($"admission-application-{key}", token);
		}

		public async Task SetAsync(Guid key, AdmissionApplication value, CancellationToken token = default)
		{
			await _cache.SetStringAsync($"admission-application-{key}", JsonSerializer.Serialize(value), new DistributedCacheEntryOptions
			{
				AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
				SlidingExpiration = TimeSpan.FromMinutes(2)
			}, token);
		}

		public async Task SetManyAsync(string key, IEnumerable<AdmissionApplication> values, CancellationToken token = default)
		{
			await _cache.SetStringAsync(key, JsonSerializer.Serialize(values), new DistributedCacheEntryOptions
			{
				AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
				SlidingExpiration = TimeSpan.FromMinutes(2)
			}, token);
		}
	}
}
