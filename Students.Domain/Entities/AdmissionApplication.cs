using Students.Domain.Enums;

namespace Students.Domain.Entities
{
	public class AdmissionApplication
	{
		public Guid Id { get; set; }
		public string ProgramCode { get; set; } = string.Empty;
		public AdmissionStatus Status { get; set; }
		public DateTime AppliedDate { get; set; }
		public Guid StudentId { get; set; }
		public Student Student { get; set; } = new();
	}
}
