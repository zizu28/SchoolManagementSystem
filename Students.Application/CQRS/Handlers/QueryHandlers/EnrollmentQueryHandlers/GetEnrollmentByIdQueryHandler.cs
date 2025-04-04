using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Hybrid;
using Students.Application.Contracts;
using Students.Application.CQRS.Queries.EnrollmentsQueries;
using Students.Application.DTOs.ResponseDTOs;
using Students.Application.Exceptions;

namespace Students.Application.CQRS.Handlers.QueryHandlers.EnrollmentQueryHandlers
{
	public class GetEnrollmentByIdQueryHandler(
		IEnrollmentRepository enrollmentRepository, 
		IMapper mapper) : IRequestHandler<GetEnrollmentByIdQuery, EnrollmentResponseDto>
	{
		private readonly IEnrollmentRepository _enrollmentRepository = enrollmentRepository;
		private readonly IMapper _mapper = mapper;

		public async Task<EnrollmentResponseDto> Handle(GetEnrollmentByIdQuery request, CancellationToken myToken)
		{
			var enrollment = await _enrollmentRepository.GetByIdAsync(request.EnrollmentId, myToken)
				?? throw new NotFoundException($"Enrollment with ID {request.EnrollmentId} not found.");
			return _mapper.Map<EnrollmentResponseDto>(enrollment);
		}
	}
}
