using MediatR;

namespace Students.Application.CQRS.Commands.EnrollmentCommands
{
	public class DeleteEnrollmentCommand : IRequest<Unit>
	{
		public Guid EnrollmentId { get; set; }
	}
}
