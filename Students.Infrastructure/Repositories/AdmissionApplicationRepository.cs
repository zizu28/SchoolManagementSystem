using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Students.Application.Contracts;
using Students.Domain.Entities;
using Students.Infrastructure.CacheServices.AdmissionApplicationCache;
using Students.Infrastructure.Data;

namespace Students.Infrastructure.Repositories
{
	public class AdmissionApplicationRepository(StudentDbContext context,
		IAdmissionApplicationCache cacheService)
		: IAdmissionApplicationRepository
	{
		private readonly StudentDbContext _context = context;
		private readonly IAdmissionApplicationCache _cacheService = cacheService;

		public async Task AddAsync(AdmissionApplication entity, CancellationToken token = default)
		{
			await _context.AdmissionApplications.AddAsync(entity, token);
			await _context.SaveChangesAsync(token);
			await _cacheService.SetAsync(entity.Id, entity, token);
		}

		public async Task AddManyAsync(IEnumerable<AdmissionApplication> entities, CancellationToken token = default)
		{
			await _context.AdmissionApplications.AddRangeAsync(entities, token);
			await _context.SaveChangesAsync(token);
			await _cacheService.SetManyAsync("admission-applications", entities, token);
		}

		public async Task DeleteAsync(AdmissionApplication entity, CancellationToken token = default)
		{
			await _cacheService.RemoveAsync(entity.Id, token);
			_context.AdmissionApplications.Remove(entity);
			await _context.SaveChangesAsync(token);
		}

		public async Task<IEnumerable<AdmissionApplication>> GetAllAsync(CancellationToken token = default)
		{
			var admissionApplications = await _context.AdmissionApplications
					.AsNoTracking()
					.OrderBy(admissionApplications => admissionApplications.AppliedDate)
					.ToListAsync(token);
			return admissionApplications;
		}

		public async Task<AdmissionApplication> GetByIdAsync(Guid Id, CancellationToken token = default)
		{
			var entity = await _context.AdmissionApplications
					.AsNoTracking()
					.FirstOrDefaultAsync(a => a.Id == Id, token);
			return entity!;
		}

		public async Task UpdateAsync(AdmissionApplication entity, CancellationToken token = default)
		{
			await _cacheService.RemoveAsync(entity.Id, token);
			_context.Entry(entity).State = EntityState.Modified;
			await _context.SaveChangesAsync(token);
			await _cacheService.SetAsync(entity.Id, entity, token);
		}
	}
}
