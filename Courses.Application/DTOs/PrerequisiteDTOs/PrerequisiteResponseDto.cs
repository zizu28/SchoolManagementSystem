namespace Courses.Application.DTOs.PrerequisiteDTOs
{
	public record PrerequisiteResponseDto(
		Guid CourseId,         // Parent course requiring the prerequisite
		CourseBasicDto RequiredCourse
	);
}
