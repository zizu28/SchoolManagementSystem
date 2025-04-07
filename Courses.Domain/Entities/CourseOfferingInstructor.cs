using Courses.Domain.Enums;

namespace Courses.Domain.Entities
{
	public class CourseOfferingInstructor
	{
		public Guid CourseOfferingId { get; set; }
		public Guid StaffId { get; set; }
		public RoleTypes Role { get; set; }
	}
}
