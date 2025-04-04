using Students.Domain.Enums;

namespace Students.Application.DTOs.EntityUpdateDtTOs
{
	public record EnrollmentUpdateDto(
		Guid EnrollmentId,
		Guid StudentId,
		Guid CourseSectionId,
		string Status,
		DateTime EnrollmentDate);
}
