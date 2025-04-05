using Microsoft.EntityFrameworkCore;
using Students.Application.Contracts;
using Students.Domain.Entities;
using Students.Infrastructure.Data;

namespace Students.Infrastructure.Repositories
{
	public class StudentRepository(StudentDbContext context) 
		: IStudentRepository
	{
		private readonly StudentDbContext _context = context;

		public async Task AddAsync(Student entity, CancellationToken token = default)
		{
			await _context.Students.AddAsync(entity, token);
			await _context.SaveChangesAsync(token);
		}

		public async Task DeleteAsync(Student entity, CancellationToken token = default)
		{
			_context.Students.Remove(entity);
			await _context.SaveChangesAsync(token);
		}

		public async Task<IEnumerable<Student>> GetAllAsync(CancellationToken token = default)
		{
			var students = await _context.Students.AsNoTracking().ToListAsync(token);
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
			_context.Entry(entity).State = EntityState.Detached;
			_context.Students.Update(entity);
			await _context.SaveChangesAsync(token);
		}
	}
}
