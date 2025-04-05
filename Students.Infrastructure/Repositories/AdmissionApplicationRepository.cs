using Microsoft.EntityFrameworkCore;
using Students.Application.Contracts;
using Students.Domain.Entities;
using Students.Infrastructure.Data;

namespace Students.Infrastructure.Repositories
{
	public class AdmissionApplicationRepository(StudentDbContext context)
		: IAdmissionApplicationRepository
	{
		private readonly StudentDbContext _context = context;

		public async Task AddAsync(AdmissionApplication entity, CancellationToken token = default)
		{
			await _context.AdmissionApplications.AddAsync(entity, token);
			await _context.SaveChangesAsync(token);
		}

		public async Task DeleteAsync(AdmissionApplication entity, CancellationToken token = default)
		{
			_context.AdmissionApplications.Remove(entity);
			await _context.SaveChangesAsync(token);
		}

		public async Task<IEnumerable<AdmissionApplication>> GetAllAsync(CancellationToken token = default)
		{
			var admissionApplications = await _context.AdmissionApplications
					.AsNoTracking()
					.ToListAsync(token);
			return admissionApplications;
		}

		public async Task<AdmissionApplication> GetByIdAsync(Guid Id, CancellationToken token = default)
		{
			var entity = await _context!.AdmissionApplications!.FirstOrDefaultAsync(a => a.Id == Id, token);
			return entity!;
		}

		public async Task UpdateAsync(AdmissionApplication entity, CancellationToken token = default)
		{
			_context.Entry(entity).State = EntityState.Detached;
			_context.AdmissionApplications.Update(entity);
			await _context.SaveChangesAsync(token);
		}
	}
}
