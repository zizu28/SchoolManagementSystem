using MediatR;
using Students.Application.DTOs.EntityCreateDTOs;
using Students.Application.Responses;

namespace Students.Application.CQRS.Commands.EnrollmentCommands
{
	public class CreateEnrollmentCommand : IRequest<BaseCommandResponse>
	{
		public EnrollmentCreateDto? Enrollment { get; set; }
	}
}
