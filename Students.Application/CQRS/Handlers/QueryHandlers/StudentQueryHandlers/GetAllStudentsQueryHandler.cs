using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Hybrid;
using Students.Application.Contracts;
using Students.Application.CQRS.Queries.StudentsQueries;
using Students.Application.DTOs.ResponseDTOs;

namespace Students.Application.CQRS.Handlers.QueryHandlers.StudentQueryHandlers
{
	public class GetAllStudentsQueryHandler( 
		IStudentRepository studentRepository,
		IMapper mapper)
				: IRequestHandler<GetAllStudentsQuery, IEnumerable<StudentResponseDto>>
	{
		private readonly IStudentRepository _studentRepository = studentRepository;
		private readonly IMapper _mapper = mapper;

		public async Task<IEnumerable<StudentResponseDto>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
		{
			var students = await _studentRepository.GetAllAsync(cancellationToken);

			return _mapper.Map<IEnumerable<StudentResponseDto>>(students);
		}
	}
}
