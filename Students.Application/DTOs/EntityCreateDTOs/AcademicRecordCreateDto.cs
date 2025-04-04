namespace Students.Application.DTOs.EntityCreateDTOs
{
	public record AcademicRecordUpddateDto(
		Guid StudentId,
		decimal GPA,
		int TotalCredits);
}
