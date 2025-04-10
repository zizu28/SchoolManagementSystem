namespace Courses.Application.DTOs.SyllabusDTOs
{
	public record SyllabusResponseDto(
		Guid SyllabusId,
		CourseBasicDto Course,
		string LearningOutcomes,
		string Textbooks,
		string GradingPolicy,
		DateTime LastUpdated);
}
