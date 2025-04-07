namespace Students.Application.DTOs.EntityCreateDTOs
{
	public record EnrollmentCreateDto(
		Guid StudentId,
		Guid CourseSectionId,
		string Status,
		DateTime EnrollmentDate);
}
