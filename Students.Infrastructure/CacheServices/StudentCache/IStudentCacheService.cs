using Students.Application.DTOs.ResponseDTOs;
using Students.Domain.Entities;

namespace Students.Infrastructure.CacheServices.StudentCache
{
	public interface IStudentCacheService : IGenericCacheService<Student,StudentResponseDto>
	{
	}
}
