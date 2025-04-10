namespace Courses.Application.DTOs.SyllabusDTOs
{
	public record SyllabusCreateDto(
		string LearningOutcomes,
		string Textbooks,
		string GradingPolicy);
}
