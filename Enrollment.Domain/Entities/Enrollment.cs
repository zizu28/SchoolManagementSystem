using Enrollment.Domain.Enums;

namespace Enrollment.Domain.Entities
{
	public class Enrollment
	{
		public Guid EnrollmentId { get; set; }
		public Guid StudentId { get; set; }
		public Guid CourseId { get; set; }
		public DateTime EnrollmentDate { get; set; }
		public string? Grade { get; set; }
		public EnrollmentStatus EnrollmentStatus { get; set; }
	}
}
