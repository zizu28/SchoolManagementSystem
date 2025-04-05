namespace Students.Application.DTOs.EntityCreateDTOs
{
	public record AcademicRecordCreateDto(
		Guid StudentId,
		decimal GPA,
		int TotalCredits);
}
