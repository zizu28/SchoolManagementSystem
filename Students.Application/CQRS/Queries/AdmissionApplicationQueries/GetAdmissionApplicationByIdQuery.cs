using MediatR;
using Students.Application.DTOs.ResponseDTOs;

namespace Students.Application.CQRS.Queries.AdmissionApplicationQueries
{
	public class GetAdmissionApplicationByIdQuery : IRequest<AdmissionApplicationResponseDto>
	{
		public Guid AdmissionApplicationId { get; set; }
	}
}
