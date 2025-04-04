namespace Students.Application.DTOs.EntityUpdateDtTOs
{
	public record AcademicRecordUpdateDto(
		Guid AcademicRecordId,
		Guid StudentId,
		decimal GPA,
		int TotalCredits);
}
