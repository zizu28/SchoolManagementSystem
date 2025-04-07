namespace Students.Domain.Entities
{
	public class Student
	{
		public Guid StudentId { get; set; }
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string Email { get; set; } = default!; 
		public DateOnly DateOfBirth { get; set; }
		public DateTime AdmissionDate { get; set; } = DateTime.UtcNow;
		public bool IsActive { get; set; }

		public List<AcademicRecord> AcademicRecords = [];
		public List<AdmissionApplication> Applications { get; set; } = [];
		public List<Enrollment> Enrollments { get; set; } = [];
	}
}
