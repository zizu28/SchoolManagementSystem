namespace Courses.Domain.Entities
{
	public class Course
	{
		public Guid CourseId { get; set; }
		public string CourseCode { get; set; } = string.Empty;
		public string Title { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public int Credits { get; set; }
		public string Department { get; set; } = string.Empty;
		public string Instructor { get; set; } = string.Empty;
		public List<Guid> StudentIds { get; set; } = [];
		public List<Guid> EnrollmentIds { get; set; } = [];
	}
}
