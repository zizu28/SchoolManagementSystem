namespace Courses.Domain.Entities
{
	public class Prerequisite
	{
		public Guid CourseId { get; set; }
		public Guid RequiredCourseId { get; set; }

		public Course Course { get; set; } = new();
		public Course RequiredCourse { get; set; } = new();
	}
}
