using MediatR;

namespace Students.Application.CQRS.Commands.StudentCommands
{
	public class DeleteStudentCommand : IRequest<Unit>
	{
		public Guid StudentId { get; set; }
	}
}
