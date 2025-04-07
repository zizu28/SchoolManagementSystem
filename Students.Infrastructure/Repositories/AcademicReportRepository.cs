using Microsoft.EntityFrameworkCore;
using Students.Application.Contracts;
using Students.Domain.Entities;
using Students.Infrastructure.CacheServices.AcademicRecordCache;
using Students.Infrastructure.Data;

namespace Students.Infrastructure.Repositories
{
	public class AcademicReportRepository(StudentDbContext context,
		IAcademicRecordCacheService cacheService)
		: IAcademicRecordRepository
	{
		private readonly StudentDbContext _context = context;
		private readonly IAcademicRecordCacheService _cacheService = cacheService;

		public async Task AddAsync(AcademicRecord entity, CancellationToken token = default)
		{
			await _context.AcademicRecords.AddAsync(entity, token);
			await _context.SaveChangesAsync(token);
			await _cacheService.SetAsync(entity.AcademicRecordId, entity, token);
		}

		public async Task AddManyAsync(IEnumerable<AcademicRecord> entities, CancellationToken token = default)
		{
			await _context.AcademicRecords.AddRangeAsync(entities, token);
			await _context.SaveChangesAsync(token);
			await _cacheService.SetManyAsync("academic-records", entities, token);
		}

		public async Task DeleteAsync(AcademicRecord entity, CancellationToken token = default)
		{
			await _cacheService.RemoveAsync(entity.AcademicRecordId, token);
			_context.AcademicRecords.Remove(entity);
			await _context.SaveChangesAsync(token);
		}

		public async Task<IEnumerable<AcademicRecord>> GetAllAsync(CancellationToken token = default)
		{
			var academicRecords = await _context.AcademicRecords
					.AsNoTracking()
					.OrderBy(a => a.Student.FirstName)
					.ToListAsync(token);
			return academicRecords;
		}

		public async Task<AcademicRecord> GetByIdAsync(Guid Id, CancellationToken token = default)
		{
			var academicRecord = await _context.AcademicRecords
					.AsNoTracking()
					.FirstOrDefaultAsync(a => a.AcademicRecordId == Id, token);
			return academicRecord!;
		}

		public async Task UpdateAsync(AcademicRecord entity, CancellationToken token = default)
		{
			await _cacheService.RemoveAsync(entity.AcademicRecordId, token);
			_context.Entry(entity).State = EntityState.Modified;
			await _context.SaveChangesAsync(token);
			await _cacheService.SetAsync(entity.AcademicRecordId, entity, token);
		}
	}
}
