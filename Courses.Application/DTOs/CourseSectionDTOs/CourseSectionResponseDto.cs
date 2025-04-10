namespace Courses.Application.DTOs.CourseSectionDTOs
{
	// Response
	public record CourseSectionResponseDto(
		Guid Id,
		CourseBasicDto Course,
		Guid FacultyId,  // Reference to external module (HR)
		string RoomNumber,
		DateTime StartDate,
		DateTime EndDate,
		int MaxCapacity,
		List<ScheduleSlotResponseDto> ScheduleSlots);
}
