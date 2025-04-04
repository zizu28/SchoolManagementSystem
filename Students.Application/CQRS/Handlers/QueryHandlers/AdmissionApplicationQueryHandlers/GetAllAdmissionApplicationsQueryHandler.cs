using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Hybrid;
using Students.Application.Contracts;
using Students.Application.CQRS.Queries.AdmissionApplicationQueries;
using Students.Application.DTOs.ResponseDTOs;

namespace Students.Application.CQRS.Handlers.QueryHandlers.AdmissionApplicationQueryHandlers
{
	public class GetAllAdmissionApplicationsQueryHandler(
		IAdmissionApplicationRepository applicationRepository, 
		IMapper mapper)
				: IRequestHandler<GetAllAdmissionApplicationsQuery, IEnumerable<AdmissionApplicationResponseDto>>
	{
		private readonly IAdmissionApplicationRepository _applicationRepository = applicationRepository;
		private readonly IMapper _mapper = mapper;

		public async Task<IEnumerable<AdmissionApplicationResponseDto>> Handle(GetAllAdmissionApplicationsQuery request, CancellationToken cancellationToken)
		{
			var admissionApplications = await _applicationRepository.GetAllAsync(cancellationToken);
			return _mapper.Map<IEnumerable<AdmissionApplicationResponseDto>>(admissionApplications);
		}
	}
}
