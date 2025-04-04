using MediatR;

namespace Students.Application.CQRS.Commands.AcademicRecordsCommands
{
	public class DeleteAcademicRecordCommand : IRequest<Unit>
	{
		public Guid AcademicRecordId { get; set; }
	}
}
