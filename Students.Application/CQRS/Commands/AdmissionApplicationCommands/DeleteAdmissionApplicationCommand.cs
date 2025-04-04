using MediatR;

namespace Students.Application.CQRS.Commands.AdmissionApplicationCommands
{
	public class DeleteAdmissionApplicationCommand : IRequest<Unit>
	{
		public Guid AdmissionApplicationId { get; set; }
	}
}
