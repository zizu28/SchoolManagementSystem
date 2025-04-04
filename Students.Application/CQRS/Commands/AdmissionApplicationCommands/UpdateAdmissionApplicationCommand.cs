using MediatR;
using Students.Application.DTOs.EntityUpdateDtTOs;
using Students.Application.Responses;

namespace Students.Application.CQRS.Commands.AdmissionApplicationCommands
{
	public class UpdateAdmissionApplicationCommand : IRequest<BaseCommandResponse>
	{
		public Guid AdmissionApplicationId { get; set; }
		public AdmissionApplicationUpdateDto? AdmissionApplication { get; set; }
	}
}
