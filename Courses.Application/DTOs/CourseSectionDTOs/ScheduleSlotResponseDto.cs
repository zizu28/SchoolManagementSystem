namespace Courses.Application.DTOs.CourseSectionDTOs
{
	public record ScheduleSlotResponseDto(
	Guid Id,
	DayOfWeek DayOfWeek,
	string TimeSlot);
}
