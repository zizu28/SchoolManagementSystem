using Courses.Domain.Enums;

namespace Courses.Application.DTOs.CourseDTOs
{
	public record CourseResponseDto(
		Guid CourseId,
		string Code,
		string Title,
		string Description,
		int Credits,
		CourseType Type,
		Guid DepartmentId);
}
