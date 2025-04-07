using Courses.Domain.Enums;

namespace Courses.Domain.Entities
{
	public class AcademicTerm
	{
		public Guid AcademicTermId { get; set; }
		public string? Name { get; set; }
		public string? Code { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public AcademicTermStatus Status { get; set; }
		public ICollection<CourseOffering> CourseOfferings { get; set; } = [];
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
		public DateOnly RegistrationStartDate { get; set; }
		public DateOnly RegistrationEndDate { get; set; }
		public DateOnly AddDropDeadline { get; set; }
	}
}
