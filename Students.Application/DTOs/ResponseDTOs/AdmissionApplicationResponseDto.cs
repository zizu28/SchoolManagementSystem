using Students.Domain.Enums;

namespace Students.Application.DTOs.ResponseDTOs
{
	public record AdmissionApplicationResponseDto(
		Guid ApplicationId,
		Guid StudentId,
		string ProgramCode,
		AdmissionStatus Status,
		DateTime AppliedDate);
}
