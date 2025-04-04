using Microsoft.EntityFrameworkCore;
using Students.Application.Contracts;
using Students.Domain.Entities;
using Students.Infrastructure.CacheServices.EnrollmentCache;
using Students.Infrastructure.Data;

namespace Students.Infrastructure.Repositories
{
	public class EnrollmentRepository(StudentDbContext dbContext, EnrollmentCacheService cacheService)
		: IEnrollmentRepository
	{
		private readonly StudentDbContext _dbContext = dbContext;
		private readonly EnrollmentCacheService _cacheService = cacheService;

		public async Task AddAsync(Enrollment entity, CancellationToken token = default)
		{
			await _dbContext.Enrollments.AddAsync(entity, token);
			await _dbContext.SaveChangesAsync(token);
			await _cacheService.SetAsync(entity.EnrollmentId, entity, token);
		}

		public async Task DeleteAsync(Enrollment entity, CancellationToken token = default)
		{
			await _cacheService.RemoveAsync(entity.EnrollmentId, token);
			_dbContext.Enrollments.Remove(entity);
			await _dbContext.SaveChangesAsync(token);
		}

		public async Task<IEnumerable<Enrollment>> GetAllAsync(CancellationToken token = default)
		{
			var enrollments = await _cacheService.GetAllAsync("enrollments", token);
			if(enrollments is null)
			{
				enrollments = await _dbContext.Enrollments.AsNoTracking().ToListAsync(token);
				await _cacheService.SetManyAsync("enrollments", enrollments, token);
			}
			return enrollments;
		}

		public async Task<Enrollment> GetByIdAsync(Guid Id, CancellationToken token = default)
		{
			var enrollment = await _cacheService.GetAsync(Id, token);
			if(enrollment is null)
			{
				enrollment = await _dbContext.Enrollments.AsNoTracking()
								.FirstOrDefaultAsync(e => e.EnrollmentId == Id, token);
				await _cacheService.SetAsync(Id, enrollment!, token);
			}
			return enrollment!;
		}

		public async Task UpdateAsync(Enrollment entity, CancellationToken token = default)
		{
			_dbContext.Entry(entity).State = EntityState.Detached;
			_dbContext.Enrollments.Update(entity);
			await _dbContext.SaveChangesAsync(token);
		}
	}
}
