namespace Courses.Domain.Entities
{
	public class Syllabus
	{
		public Guid Id { get; set; }
		public Guid CourseId { get; set; }
		public string LearningOutcomes { get; set; } = string.Empty;
		public string Textbooks { get; set; } = string.Empty;
		public string GradingPolicy { get; set; } = string.Empty;

		public Course Course { get; set; } = new();
	}
}
