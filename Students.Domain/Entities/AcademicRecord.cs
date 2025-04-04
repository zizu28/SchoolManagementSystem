namespace Students.Domain.Entities
{
	public class AcademicRecord
	{
		public Guid AcademicRecordId { get; set; }
		public decimal GPA { get; set; }
		public int TotalCredits { get; set; }
		public Guid StudentId { get; set; }
		public Student Student { get; set; } = new();
	}
}
