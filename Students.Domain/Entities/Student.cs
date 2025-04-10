using Core.Identity.Abstractions;
using Students.Domain.Enums;

namespace Students.Domain.Entities
{
	public class Student(Guid studentId, string firstName, string lastName, string email) : IUser
	{
		public Guid StudentId { get; private set; } = studentId;
		public string FirstName { get; private set; } = firstName;
		public string LastName { get; private set; } = lastName;
		public DateTime DateOfBirth { get; private set; }
		public string Email { get; private set; } = email;
		public string PhoneNumber { get; private set; } = string.Empty;
		public string Address { get; private set; } = string.Empty;
		public DateTime EnrollmentDate { get; private set; }
		public string Major { get; private set; } = string.Empty;
		public string? Minor { get; private set; }
		public StudentStatus StudentStatus { get; private set; }
		public List<Guid> CourseIds { get; private set; } = [];
		public List<Guid> EnrollmentIds { get; private set; } = [];

		public Guid Id => StudentId;

		string IUser.UserName => Email;
	}
}
