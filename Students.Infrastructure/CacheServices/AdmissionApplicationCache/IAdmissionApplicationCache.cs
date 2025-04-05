using Students.Application.DTOs.ResponseDTOs;
using Students.Domain.Entities;

namespace Students.Infrastructure.CacheServices.AdmissionApplicationCache
{
	public interface IAdmissionApplicationCache : IGenericCacheService<AdmissionApplication, AdmissionApplicationResponseDto>
	{
	}
}
