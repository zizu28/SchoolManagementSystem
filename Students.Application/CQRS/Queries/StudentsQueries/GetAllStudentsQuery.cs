using MediatR;
using Students.Application.DTOs.ResponseDTOs;

namespace Students.Application.CQRS.Queries.StudentsQueries
{
	public class GetAllStudentsQuery : IRequest<IEnumerable<StudentResponseDto>>
	{
	}
}
