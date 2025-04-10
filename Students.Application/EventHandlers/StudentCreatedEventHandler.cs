using MassTransit;
using MediatR;
using Students.Application.Events;

namespace Students.Application.EventHandlers
{
	public class StudentCreatedEventHandler(IPublishEndpoint publishEndpoint) : INotificationHandler<StudentCreatedEvent>
	{
		private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

		public Task Handle(StudentCreatedEvent notification, CancellationToken cancellationToken)
		{
			var integrationEvent = new StudentCreatedEvent(notification.StudentId, notification.Email);
			return _publishEndpoint.Publish(integrationEvent, cancellationToken);
		}
	}
}
