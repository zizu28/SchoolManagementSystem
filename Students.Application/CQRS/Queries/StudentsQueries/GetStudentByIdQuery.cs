using MediatR;
using Students.Application.DTOs.ResponseDTOs;

namespace Students.Application.CQRS.Queries.StudentsQueries
{
	public class GetStudentByIdQuery : IRequest<StudentResponseDto>
	{
		public Guid StudentId { get; set; }
	}
}
