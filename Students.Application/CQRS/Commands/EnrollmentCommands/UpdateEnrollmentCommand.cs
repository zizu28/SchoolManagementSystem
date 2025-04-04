using MediatR;
using Students.Application.DTOs.EntityUpdateDtTOs;
using Students.Application.Responses;

namespace Students.Application.CQRS.Commands.EnrollmentCommands
{
	public class UpdateEnrollmentCommand : IRequest<BaseCommandResponse>
	{
		public Guid EnrollmentId { get; set; }
		public EnrollmentUpdateDto? Enrollment { get; set; }
	}
}
