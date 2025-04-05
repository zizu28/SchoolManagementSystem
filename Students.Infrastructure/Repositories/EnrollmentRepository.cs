using Microsoft.EntityFrameworkCore;
using Students.Application.Contracts;
using Students.Domain.Entities;
using Students.Infrastructure.Data;

namespace Students.Infrastructure.Repositories
{
	public class EnrollmentRepository(StudentDbContext dbContext)
		: IEnrollmentRepository
	{
		private readonly StudentDbContext _dbContext = dbContext;

		public async Task AddAsync(Enrollment entity, CancellationToken token = default)
		{
			await _dbContext.Enrollments.AddAsync(entity, token);
			await _dbContext.SaveChangesAsync(token);
		}

		public async Task DeleteAsync(Enrollment entity, CancellationToken token = default)
		{
			_dbContext.Enrollments.Remove(entity);
			await _dbContext.SaveChangesAsync(token);
		}

		public async Task<IEnumerable<Enrollment>> GetAllAsync(CancellationToken token = default)
		{
			var enrollments = await _dbContext.Enrollments.AsNoTracking().ToListAsync(token);
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
			_dbContext.Entry(entity).State = EntityState.Detached;
			_dbContext.Enrollments.Update(entity);
			await _dbContext.SaveChangesAsync(token);
		}
	}
}
