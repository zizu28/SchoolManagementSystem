using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Hybrid;
using Students.Application.Contracts;
using Students.Application.CQRS.Queries.EnrollmentsQueries;
using Students.Application.DTOs.ResponseDTOs;

namespace Students.Application.CQRS.Handlers.QueryHandlers.EnrollmentQueryHandlers
{
	public class GetAllEnrollmentsQueryHandler(
		IEnrollmentRepository enrollmentRepository,
		IMapper mapper) : IRequestHandler<GetAllEnrollmentsQuery, IEnumerable<EnrollmentResponseDto>>
	{
		private readonly IEnrollmentRepository _enrollmentRepository = enrollmentRepository;
		private readonly IMapper _mapper = mapper;

		public async Task<IEnumerable<EnrollmentResponseDto>> Handle(GetAllEnrollmentsQuery request, CancellationToken cancellationToken)
		{
			var enrollments = await _enrollmentRepository.GetAllAsync(cancellationToken);
			return _mapper.Map<IEnumerable<EnrollmentResponseDto>>(enrollments);
		}
	}
}
