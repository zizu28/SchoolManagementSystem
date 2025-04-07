using Courses.Domain.Enums;

namespace Courses.Domain.Entities
{
	public class Course
	{
		public Guid CourseId { get; set; }
		public string? CourseCode { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
		public int Credits { get; set; }
		public ICollection<CourseOffering> CourseOfferings { get; set; } = [];
		public ICollection<Course> CoursePrerequisites { get; set; } = [];
		public Guid DepartmentId { get; set; }
		public EducationLevel Level { get; set; }
		public bool IsActive { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
	}
}
