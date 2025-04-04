using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Hybrid;
using Students.Application.Contracts;
using Students.Application.CQRS.Queries.AcademicRecordsQueries;
using Students.Application.DTOs.ResponseDTOs;
using Students.Application.Exceptions;

namespace Students.Application.CQRS.Handlers.QueryHandlers.AcademicRecordQueryHandlers
{
	public class GetAcademicRecordByIdQueryHandler(
		IAcademicRecordRepository recordRepository,
		IMapper mapper)
		: IRequestHandler<GetAcademicRecordByIdQuery, AcademicRecordResponseDto>
	{
		private readonly IAcademicRecordRepository _recordRepository = recordRepository;
		private readonly IMapper _mapper = mapper;
		public async Task<AcademicRecordResponseDto> Handle(GetAcademicRecordByIdQuery request, CancellationToken cancellationToken)
		{
			var academicRecord = await _recordRepository.GetByIdAsync(request.AcademicRecordId, cancellationToken)
				?? throw new NotFoundException($"Academic record with id {request.AcademicRecordId} not found.");
			return _mapper.Map<AcademicRecordResponseDto>(academicRecord);
		}
	}
}
