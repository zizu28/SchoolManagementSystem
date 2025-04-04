using MediatR;
using Students.Application.DTOs.ResponseDTOs;

namespace Students.Application.CQRS.Queries.AcademicRecordsQueries
{
	public class GetAllAcademicRecordsQuery : IRequest<IEnumerable<AcademicRecordResponseDto>>
	{
	}
}
