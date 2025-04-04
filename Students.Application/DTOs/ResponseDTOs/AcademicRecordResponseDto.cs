namespace Students.Application.DTOs.ResponseDTOs
{
	public record AcademicRecordResponseDto(
		Guid AcademicRecordId,
		Guid StudentId,
		decimal GPA,
		int TotalCredits);
}
