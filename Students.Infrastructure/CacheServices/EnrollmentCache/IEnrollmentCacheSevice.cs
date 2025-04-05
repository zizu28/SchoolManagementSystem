using Students.Application.DTOs.ResponseDTOs;
using Students.Domain.Entities;

namespace Students.Infrastructure.CacheServices.EnrollmentCache
{
	public interface IEnrollmentCacheSevice : IGenericCacheService<Enrollment, EnrollmentResponseDto>
	{
	}
}
