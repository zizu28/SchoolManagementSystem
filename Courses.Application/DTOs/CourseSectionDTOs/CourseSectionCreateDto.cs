namespace Courses.Application.DTOs.CourseSectionDTOs
{
	public record CourseSectionCreateDto(
	Guid CourseId,
	Guid FacultyId,
	string RoomNumber,
	DateTime StartDate,
	DateTime EndDate,
	int MaxCapacity,
	List<ScheduleSlotCreateDto> ScheduleSlots);
}
