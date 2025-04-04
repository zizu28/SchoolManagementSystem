using Microsoft.EntityFrameworkCore;
using Students.Application.Contracts;
using Students.Domain.Entities;
using Students.Infrastructure.CacheServices.AcademicRecordCache;
using Students.Infrastructure.Data;

namespace Students.Infrastructure.Repositories
{
	public class AcademicReportRepository(StudentDbContext context, AcademicRecordCacheService cacheService)
		: IAcademicRecordRepository
	{
		private readonly StudentDbContext _context = context;
		private readonly AcademicRecordCacheService _cacheService = cacheService;

		public async Task AddAsync(AcademicRecord entity, CancellationToken token = default)
		{
			await _context.AcademicRecords.AddAsync(entity, token);
			await _context.SaveChangesAsync(token);
			await _cacheService.SetAsync(entity.AcademicRecordId, entity, token);
		}

		public async Task DeleteAsync(AcademicRecord entity, CancellationToken token = default)
		{
			await _cacheService.RemoveAsync(entity.AcademicRecordId, token);
			_context.AcademicRecords.Remove(entity);
			await _context.SaveChangesAsync(token);
		}

		public async Task<IEnumerable<AcademicRecord>> GetAllAsync(CancellationToken token = default)
		{
			var academicRecords = await _cacheService.GetAllAsync("academic-records", token)
				?? await _context.AcademicRecords.AsNoTracking().ToListAsync(token);
			await _cacheService.SetManyAsync("academic-records", academicRecords, token);
			return academicRecords;
		}

		public async Task<AcademicRecord> GetByIdAsync(Guid Id, CancellationToken token = default)
		{
			var academicRecord = await _cacheService.GetAsync(Id, token);
			if(academicRecord is null)
			{
				academicRecord = await _context.AcademicRecords.AsNoTracking()
					.FirstOrDefaultAsync(a => a.AcademicRecordId == Id, token);
				await _cacheService.SetAsync(Id, academicRecord!, token);
			}
			return academicRecord!;
		}

		public async Task UpdateAsync(AcademicRecord entity, CancellationToken token = default)
		{
			_context.Entry(entity).State = EntityState.Detached;
			_context.AcademicRecords.Update(entity);
			await _context.SaveChangesAsync(token);
		}
	}
}
