using MediatR;
using Students.Application.DTOs.ResponseDTOs;

namespace Students.Application.CQRS.Queries.EnrollmentsQueries
{
	public class GetEnrollmentByIdQuery : IRequest<EnrollmentResponseDto> 
	{
		public Guid EnrollmentId { get; set; }
	}
}
