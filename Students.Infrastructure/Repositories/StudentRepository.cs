using Microsoft.EntityFrameworkCore;
using Students.Application.Contracts;
using Students.Domain.Entities;
using Students.Infrastructure.CacheServices.StudentCache;
using Students.Infrastructure.Data;

namespace Students.Infrastructure.Repositories
{
	public class StudentRepository(StudentDbContext context,
		IStudentCacheService cacheService) 
		: IStudentRepository
	{
		private readonly StudentDbContext _context = context;
		private readonly IStudentCacheService _cacheService = cacheService;

		public async Task AddAsync(Student entity, CancellationToken token = default)
		{
			await _context.Students.AddAsync(entity, token);
			await _context.SaveChangesAsync(token);
			await _cacheService.SetAsync(entity.StudentId, entity, token);
		}

		public async Task AddManyAsync(IEnumerable<Student> entities, CancellationToken token = default)
		{
			await _context.Students.AddRangeAsync(entities, token);
			await _context.SaveChangesAsync(token);
			await _cacheService.SetManyAsync("students", entities, token);
		}

		public async Task DeleteAsync(Student entity, CancellationToken token = default)
		{
			await _cacheService.RemoveAsync(entity.StudentId, token);
			_context.Students.Remove(entity);
			await _context.SaveChangesAsync(token);
		}

		public async Task<IEnumerable<Student>> GetAllAsync(CancellationToken token = default)
		{
			var students = await _context.Students
					.AsNoTracking()
					.OrderBy(s => s.FirstName)
					.ToListAsync(token);
			return students;
		}

		public async Task<Student> GetByIdAsync(Guid Id, CancellationToken token = default)
		{
			var student = await _context.Students.AsNoTracking()
					.FirstOrDefaultAsync(s => s.StudentId == Id, token);
			return student!;
		}

		public async Task UpdateAsync(Student entity, CancellationToken token = default)
		{
			await _cacheService.RemoveAsync(entity.StudentId, token);
			_context.Entry(entity).State = EntityState.Detached;
			_context.Students.Update(entity);
			await _context.SaveChangesAsync(token);
			await _cacheService.SetAsync(entity.StudentId, entity, token);
		}
	}
}
