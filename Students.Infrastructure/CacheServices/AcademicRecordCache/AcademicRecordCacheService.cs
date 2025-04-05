using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Students.Application.Contracts;
using Students.Application.DTOs.ResponseDTOs;
using Students.Domain.Entities;
using System.Text.Json;

namespace Students.Infrastructure.CacheServices.AcademicRecordCache
{
	public class AcademicRecordCacheService(IDistributedCache cache, 
		IAcademicRecordRepository repository, IMapper mapper) 
		: IAcademicRecordCacheService
	{
		private readonly IDistributedCache _cache = cache;
		private readonly IAcademicRecordRepository _repository = repository;
		private readonly IMapper _mapper = mapper;

		public async Task<IEnumerable<AcademicRecordResponseDto>> GetAllAsync(string key, CancellationToken token = default)
		{
			var academicRecords = await _cache.GetStringAsync(key, token);
			if(academicRecords is null)
			{
				var academicRecordsFromDb = await _repository.GetAllAsync(token);
				_cache.SetString(key, JsonSerializer.Serialize(academicRecordsFromDb));
				return _mapper.Map<IEnumerable<AcademicRecordResponseDto>>(academicRecordsFromDb!);
			}
			var records = JsonSerializer.Deserialize<IEnumerable<AcademicRecord>>(academicRecords!);
			return _mapper.Map<IEnumerable<AcademicRecordResponseDto>>(records!);
		}

		public async Task<AcademicRecordResponseDto> GetAsync(Guid key, CancellationToken token = default)
		{
			var academicRecord = await _cache.GetStringAsync($"academic-record-{key}", token);
			if(academicRecord is null)
			{
				var academicRecordFromDb = await _repository.GetByIdAsync(key, token);
				await _cache.SetStringAsync($"academic-record-{key}", JsonSerializer.Serialize(academicRecordFromDb), token);
				return _mapper.Map<AcademicRecordResponseDto>(academicRecordFromDb);
			}
			var result = JsonSerializer.Deserialize<AcademicRecord>(academicRecord);
			return _mapper.Map<AcademicRecordResponseDto>(result);
		}

		public async Task RemoveAsync(Guid key, CancellationToken token = default)
		{
			await _cache.RemoveAsync($"academic-record-{key}", token);
		}

		public async Task SetAsync(Guid key, AcademicRecord value, CancellationToken token = default)
		{
			await _cache.SetStringAsync($"academic-record-{key}", JsonSerializer.Serialize(value), new DistributedCacheEntryOptions
			{
				AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
				SlidingExpiration = TimeSpan.FromMinutes(2)
			}, token);
		}

		public async Task SetManyAsync(string key, IEnumerable<AcademicRecord> values, CancellationToken token = default)
		{
			await _cache.SetStringAsync(key, JsonSerializer.Serialize(values), new DistributedCacheEntryOptions
			{
				AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
				SlidingExpiration = TimeSpan.FromMinutes(2)
			}, token);
		}
	}
}
