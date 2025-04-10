namespace Courses.Application.DTOs.SyllabusDTOs
{
	public record SyllabusUpdateDto(
		Guid SyllabusId,
		string? LearningOutcomes,
		string? Textbooks,
		string? GradingPolicy);
}
