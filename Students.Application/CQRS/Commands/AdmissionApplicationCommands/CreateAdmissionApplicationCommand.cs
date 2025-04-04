using MediatR;
using Students.Application.DTOs.EntityCreateDTOs;
using Students.Application.Responses;

namespace Students.Application.CQRS.Commands.AdmissionApplicationCommands
{
	public class CreateAdmissionApplicationCommand : IRequest<BaseCommandResponse>
	{
		public AdmissionApplicationCreateDto? AdmissionApplication { get; set; }
	}
}
