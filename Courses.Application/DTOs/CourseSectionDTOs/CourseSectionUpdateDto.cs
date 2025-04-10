namespace Courses.Application.DTOs.CourseSectionDTOs
{
	// Update
	public record CourseSectionUpdateDto(
		Guid CourseSectionId,
		Guid? CourseId,
		Guid? FacultyId,
		string? RoomNumber,
		DateTime? StartDate,
		DateTime? EndDate,
		int? MaxCapacity,
		List<ScheduleSlotUpdateDto>? ScheduleSlots);
}
