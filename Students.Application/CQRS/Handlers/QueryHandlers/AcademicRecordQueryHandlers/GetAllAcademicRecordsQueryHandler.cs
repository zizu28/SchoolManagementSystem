using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Hybrid;
using Students.Application.Contracts;
using Students.Application.CQRS.Queries.AcademicRecordsQueries;
using Students.Application.DTOs.ResponseDTOs;

namespace Students.Application.CQRS.Handlers.QueryHandlers.AcademicRecordQueryHandlers
{
	public class GetAllAcademicRecordsQueryHandler(
		IAcademicRecordRepository recordRepository, 
		IMapper mapper)
				: IRequestHandler<GetAllAcademicRecordsQuery, IEnumerable<AcademicRecordResponseDto>>
	{
		private readonly IAcademicRecordRepository _recordRepository = recordRepository;
		private readonly IMapper _mapper = mapper;

		public async Task<IEnumerable<AcademicRecordResponseDto>> Handle(GetAllAcademicRecordsQuery request, CancellationToken cancellationToken)
		{
			var academicRecords = await _recordRepository.GetAllAsync(cancellationToken);
			return _mapper.Map<IEnumerable<AcademicRecordResponseDto>>(academicRecords);
		}
	}
}
