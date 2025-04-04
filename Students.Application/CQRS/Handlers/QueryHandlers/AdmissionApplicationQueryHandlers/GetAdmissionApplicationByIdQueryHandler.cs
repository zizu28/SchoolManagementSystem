using AutoMapper;
using MediatR;
using Students.Application.Contracts;
using Students.Application.CQRS.Queries.AdmissionApplicationQueries;
using Students.Application.DTOs.ResponseDTOs;
using Students.Application.Exceptions;

namespace Students.Application.CQRS.Handlers.QueryHandlers.AdmissionApplicationQueryHandlers
{
	public class GetAdmissionApplicationByIdQueryHandler(
		IAdmissionApplicationRepository applicationRepository,
		IMapper mapper)
		: IRequestHandler<GetAdmissionApplicationByIdQuery, AdmissionApplicationResponseDto>
	{
		private readonly IAdmissionApplicationRepository _applicationRepository = applicationRepository;
		private readonly IMapper _mapper = mapper;
		public async Task<AdmissionApplicationResponseDto> Handle(GetAdmissionApplicationByIdQuery request, CancellationToken cancellationToken)
		{
			var admissionApplication = await _applicationRepository.GetByIdAsync(request.AdmissionApplicationId, cancellationToken) 
				?? throw new NotFoundException($"Admission application with id {request.AdmissionApplicationId}" +
				$" not found.");
			return _mapper.Map<AdmissionApplicationResponseDto>(admissionApplication);
		}
	}
}
