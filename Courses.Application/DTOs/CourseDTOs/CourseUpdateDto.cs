using Courses.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Courses.Application.DTOs.CourseDTOs
{
	public record CourseUpdateDto(
		Guid CourseId,
		string Code,
		string? Title,
		string? Description,
		int Credits,
		CourseType? Type,
		Guid? DepartmentId);
}
