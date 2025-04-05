using MediatR;
using Students.Application.DTOs.EntityCreateDTOs;
using Students.Application.Responses;

namespace Students.Application.CQRS.Commands.AcademicRecordsCommands
{
	public class CreateAcademicRecordCommand : IRequest<BaseCommandResponse>
	{
		public AcademicRecordCreateDto? AcademicRecord { get; set; }
	}
}
