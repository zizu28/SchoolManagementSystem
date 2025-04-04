using MediatR;
using Students.Application.DTOs.ResponseDTOs;

namespace Students.Application.CQRS.Queries.EnrollmentsQueries
{
	public class GetAllEnrollmentsQuery : IRequest<IEnumerable<EnrollmentResponseDto>>
	{
	}
}
