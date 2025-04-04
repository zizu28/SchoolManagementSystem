using Students.Domain.Enums;

namespace Students.Application.DTOs.ResponseDTOs
{
	public record EnrollmentResponseDto(
		Guid EnrollmentId,
		Guid StudentId,
		Guid CourseSectionId,
		EnrollmentStatus Status,
		DateTime EnrollmentDate);
}
