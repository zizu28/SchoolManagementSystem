using Students.Domain.Enums;

namespace Students.Application.DTOs.EntityCreateDTOs
{
	public record AdmissionApplicationCreateDto(
		Guid StudentId,
		string ProgramCode,
		string Status,
		DateTime AppliedDate);
}
