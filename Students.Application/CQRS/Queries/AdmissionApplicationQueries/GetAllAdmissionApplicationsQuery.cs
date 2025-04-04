using MediatR;
using Students.Application.DTOs.ResponseDTOs;

namespace Students.Application.CQRS.Queries.AdmissionApplicationQueries
{
	public class GetAllAdmissionApplicationsQuery : IRequest<IEnumerable<AdmissionApplicationResponseDto>>
	{
	}
}
