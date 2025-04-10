using Students.Domain.Enums;

namespace Students.Application.DTOs.ResponseDTOs
{
	public record EnrollmentResponseDto(
		Guid EnrollmentId,
		Guid StudentId,
		Guid CourseOfferingId,
		EnrollmentStatus Status,
		DateTime EnrollmentDate);
}
