using Courses.Domain.Enums;

namespace Courses.Application.DTOs.CourseSectionDTOs
{
	public record ScheduleSlotCreateDto(
	DayOfTheWeek DayOfWeek,
	TimeOnly StartTime,
	TimeOnly EndTime);
}
