using MediatR;
using Students.Application.DTOs.EntityUpdateDtTOs;
using Students.Application.Responses;

namespace Students.Application.CQRS.Commands.StudentCommands
{
	public class UpdateStudentCommand : IRequest<BaseCommandResponse>
	{
		public Guid StudentId { get; set; }
		public StudentUpdateDto? Student { get; set; }
	}
}
