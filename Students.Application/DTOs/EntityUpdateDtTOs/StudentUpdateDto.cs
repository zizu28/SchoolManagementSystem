namespace Students.Application.DTOs.EntityUpdateDtTOs
{
	public record StudentUpdateDto(
		Guid StudentId,
		string FirstName,
		string LastName,
		string Email,
		DateOnly DateOfBirth);
}
