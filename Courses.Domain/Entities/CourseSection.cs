namespace Courses.Domain.Entities
{
	public class CourseSection
	{
		public Guid Id { get; set; }
		public Guid CourseId { get; set; }
		public Guid FacultyId { get; set; } // Reference to external module
		public string RoomNumber { get; set; } = string.Empty;
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public int MaxCapacity { get; set; }

		public List<ScheduleSlot> Schedule { get; set; } = [];
		public Course Course { get; set; } = new();
	}
}
