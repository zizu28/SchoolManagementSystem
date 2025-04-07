using Microsoft.EntityFrameworkCore;
using Students.Application.Contracts;
using Students.Domain.Entities;
using Students.Infrastructure.CacheServices.EnrollmentCache;
using Students.Infrastructure.Data;

namespace Students.Infrastructure.Repositories
{
	public class EnrollmentRepository(StudentDbContext dbContext,
		IEnrollmentCacheSevice cacheService)
		: IEnrollmentRepository
	{
		private readonly StudentDbContext _dbContext = dbContext;
		private readonly IEnrollmentCacheSevice _cacheService = cacheService;

		public async Task AddAsync(Enrollment entity, CancellationToken token = default)
		{
			await _dbContext.Enrollments.AddAsync(entity, token);
			await _dbContext.SaveChangesAsync(token);
			await _cacheService.SetAsync(entity.EnrollmentId, entity, token);
		}

		public async Task AddManyAsync(IEnumerable<Enrollment> entities, CancellationToken token = default)
		{
			await _dbContext.Enrollments.AddRangeAsync(entities, token);
			await _dbContext.SaveChangesAsync(token);
			await _cacheService.SetManyAsync("enrollments", entities, token);
		}

		public async Task DeleteAsync(Enrollment entity, CancellationToken token = default)
		{
			await _cacheService.RemoveAsync(entity.EnrollmentId, token);
			_dbContext.Enrollments.Remove(entity);
			await _dbContext.SaveChangesAsync(token);
		}

		public async Task<IEnumerable<Enrollment>> GetAllAsync(CancellationToken token = default)
		{
			var enrollments = await _dbContext.Enrollments
					.AsNoTracking()
					.OrderBy(e => e.EnrollmentDate)
					.ToListAsync(token);
			return enrollments;
		}

		public async Task<Enrollment> GetByIdAsync(Guid Id, CancellationToken token = default)
		{
			var enrollment = await _dbContext.Enrollments.AsNoTracking()
								.FirstOrDefaultAsync(e => e.EnrollmentId == Id, token);
			return enrollment!;
		}

		public async Task UpdateAsync(Enrollment entity, CancellationToken token = default)
		{
			await _cacheService.RemoveAsync(entity.EnrollmentId, token);
			_dbContext.Entry(entity).State = EntityState.Modified;
			await _dbContext.SaveChangesAsync(token);
			await _cacheService.SetAsync(entity.EnrollmentId, entity, token);
		}
	}
}
