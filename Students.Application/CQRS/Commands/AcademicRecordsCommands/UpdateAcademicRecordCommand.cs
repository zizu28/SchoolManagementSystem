using MediatR;
using Students.Application.DTOs.EntityUpdateDtTOs;
using Students.Application.Responses;

namespace Students.Application.CQRS.Commands.AcademicRecordsCommands
{
	public class UpdateAcademicRecordCommand : IRequest<BaseCommandResponse>
	{
		public Guid AcademicRecordId { get; set; }
		public AcademicRecordUpdateDto? AcademicRecord { get; set; }
	}
}
