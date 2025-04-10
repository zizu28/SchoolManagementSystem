namespace Students.Application.DTOs.EntityCreateDTOs
{
	public record EnrollmentCreateDto(
		Guid StudentId,
		Guid CourseOfferingId,
		string Status,
		DateTime EnrollmentDate);
}
