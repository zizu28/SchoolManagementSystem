using Core.EventBus;

namespace Students.Application.Events
{
	public class StudentCreatedIntegrationEvent(Guid studentId, string email) : IntegrationEvent
	{
		public Guid StudentId { get; } = studentId;
		public string Email { get; } = email;
	}
}
