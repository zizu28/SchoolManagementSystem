namespace Courses.Application.DTOs.CourseSectionDTOs
{
	public record ScheduleSlotUpdateDto(
	DayOfWeek? DayOfWeek,
	TimeOnly? StartTime,
	TimeOnly? EndTime);
}
