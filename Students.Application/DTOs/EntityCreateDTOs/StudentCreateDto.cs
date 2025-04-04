namespace Students.Application.DTOs.EntityCreateDTOs
{
	public record StudentCreateDto(
		string FirstName,
		string LastName,
		string Email,
		DateOnly DateOfBirth);
}
