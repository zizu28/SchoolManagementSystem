using Courses.Domain.Enums;

namespace Courses.Domain.Entities
{
	public class CoursePrerequisite
	{
		public Guid CoursePrerequisiteId { get; set; }
		public Guid CourseId { get; set; }
		public CourseRequirementType RequirementType { get; set; }
		public string MinimumGrade { get; set; } = string.Empty;
	}
}
