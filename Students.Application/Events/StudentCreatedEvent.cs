using MediatR;

namespace Students.Application.Events
{
	public class StudentCreatedEvent(Guid studentId, string email) : INotification
	{
		public Guid StudentId { get; } = studentId;
		public string Email { get; } = email;
	}
}
