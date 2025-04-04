using MediatR;
using Students.Application.DTOs.ResponseDTOs;

namespace Students.Application.CQRS.Queries.AcademicRecordsQueries
{
	public class GetAcademicRecordByIdQuery : IRequest<AcademicRecordResponseDto>
	{
		public Guid AcademicRecordId { get; set; }
	}
}
