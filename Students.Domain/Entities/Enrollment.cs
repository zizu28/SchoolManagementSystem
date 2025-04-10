using Students.Domain.Enums;

namespace Students.Domain.Entities
{
	public class Enrollment
	{
		public Guid EnrollmentId { get; set; }
		public Guid CourseOfferingId { get; set; }
		public EnrollmentStatus Status { get; set; }
		public DateTime EnrollmentDate { get; set; }
		public Guid StudentId { get; set; }
		public Student Student { get; set; } = new();
	}
}
