using Microsoft.EntityFrameworkCore;
using Students.Application.Contracts;
using Students.Domain.Entities;
using Students.Infrastructure.CacheServices.AdmissionApplicationCache;
using Students.Infrastructure.Data;

namespace Students.Infrastructure.Repositories
{
	public class AdmissionApplicationRepository(StudentDbContext context, AdmissionApplicationCacheService cacheService)
		: IAdmissionApplicationRepository
	{
		private readonly StudentDbContext _context = context;
		private readonly AdmissionApplicationCacheService _cacheService = cacheService;

		public async Task AddAsync(AdmissionApplication entity, CancellationToken token = default)
		{
			await _context.AdmissionApplications.AddAsync(entity, token);
			await _context.SaveChangesAsync(token);
			await _cacheService.SetAsync(entity.Id, entity, token);
		}

		public async Task DeleteAsync(AdmissionApplication entity, CancellationToken token = default)
		{
			await _cacheService.RemoveAsync(entity.Id, token);
			_context.AdmissionApplications.Remove(entity);
			await _context.SaveChangesAsync(token);
		}

		public async Task<IEnumerable<AdmissionApplication>> GetAllAsync(CancellationToken token = default)
		{
			var admissionApplications = await _cacheService.GetAllAsync("admission-applications", token);
			if (admissionApplications is null)
			{
				admissionApplications = await _context.AdmissionApplications
					.AsNoTracking()
					.ToListAsync(token);
				await _cacheService.SetManyAsync("admission-applications", admissionApplications, token);
			}
			return admissionApplications;
		}

		public async Task<AdmissionApplication> GetByIdAsync(Guid Id, CancellationToken token = default)
		{
			var entity = await _cacheService.GetAsync(Id, token);
			if (entity is null)
			{
				entity = await _context!.AdmissionApplications!.FirstOrDefaultAsync(a => a.Id == Id, token);
				await _cacheService.SetAsync(Id, entity!, token);
			}
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
