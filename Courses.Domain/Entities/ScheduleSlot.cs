namespace Courses.Domain.Entities
{
	public class ScheduleSlot
	{
		public Guid Id { get; set; }
		public DayOfWeek Day { get; set; }
		public TimeOnly StartTime { get; set; }
		public TimeOnly EndTime { get; set; }
		public Guid CourseSectionId { get; set; }

		public CourseSection Section { get; set; } = new();
	}
}
