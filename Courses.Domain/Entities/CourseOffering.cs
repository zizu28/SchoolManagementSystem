using Courses.Domain.Enums;

namespace Courses.Domain.Entities
{
	public class CourseOffering
	{
		public Guid CourseOfferingId { get; set; }
		public Guid CourseId { get; set; }
		public Course Course { get; set; } = new();
		public Guid AcademicTermId { get; set; }
		public AcademicTerm AcademicTerm { get; set; } = new();
		public string? SectionIdentifier { get; set; }
		public int MaxCapacity { get; set; }
		public int CurrentEnrollment { get; set; }
		public CourseOfferingStatus Status { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
	}
}
