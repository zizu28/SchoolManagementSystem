namespace Students.Application.DTOs.ResponseDTOs
{
	public record StudentResponseDto(
		Guid StudentId,
		string FirstName,
		string LastName,
		string Email,
		bool IsActive,
		DateOnly DateOfBirth,
		List<EnrollmentResponseDto> Enrollments,
		List<AdmissionApplicationResponseDto> Applications,
		List<AcademicRecordResponseDto> AcademicRecords);
}
