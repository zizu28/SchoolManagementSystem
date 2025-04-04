using MediatR;
using Students.Application.DTOs.EntityCreateDTOs;
using Students.Application.Responses;

namespace Students.Application.CQRS.Commands.StudentCommands
{
	public class CreateStudentCommand : IRequest<BaseCommandResponse>
	{
		public StudentCreateDto? Student { get; set; }
	}
}
