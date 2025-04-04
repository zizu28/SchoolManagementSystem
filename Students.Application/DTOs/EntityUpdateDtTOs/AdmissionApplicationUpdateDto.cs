using Students.Domain.Enums;

namespace Students.Application.DTOs.EntityUpdateDtTOs
{
	public record AdmissionApplicationUpdateDto(
		Guid AdmissionApplicationId,
		Guid StudentId,
		string ProgramCode,
		string Status,
		DateTime AppliedDate);
}
