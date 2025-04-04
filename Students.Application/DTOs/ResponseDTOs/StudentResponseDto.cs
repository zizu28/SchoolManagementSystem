namespace Students.Application.DTOs.ResponseDTOs
{
	public record StudentResponseDto(
		Guid StudentId,
		Guid AcademicRecordId,
		string FirstName,
		string LastName,
		string Email,
		bool IsActive,
		DateOnly DateOfBirth,
		List<EnrollmentResponseDto> Enrollments);
}
