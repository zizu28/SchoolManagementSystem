using Students.Domain.Enums;

namespace Students.Application.DTOs.EntityUpdateDtTOs
{
	public record EnrollmentUpdateDto(
		Guid EnrollmentId,
		Guid StudentId,
		Guid CourseOfferingId,
		string Status,
		DateTime EnrollmentDate);
}
