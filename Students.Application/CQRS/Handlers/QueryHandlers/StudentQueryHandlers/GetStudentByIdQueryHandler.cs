using AutoMapper;
using MediatR;
using Students.Application.Contracts;
using Students.Application.CQRS.Queries.StudentsQueries;
using Students.Application.DTOs.ResponseDTOs;
using Students.Application.Exceptions;

namespace Students.Application.CQRS.Handlers.QueryHandlers.StudentQueryHandlers
{
	public class GetStudentByIdQueryHandler( 
		IStudentRepository studentRepository,
		IMapper mapper) 
		: IRequestHandler<GetStudentByIdQuery, StudentResponseDto>
	{
		private readonly IStudentRepository _studentRepository = studentRepository;
		private readonly IMapper _mapper = mapper;
		public async Task<StudentResponseDto> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
		{
			var student = await _studentRepository.GetByIdAsync(request.StudentId, cancellationToken) 
				?? throw new NotFoundException($"Student with id {request.StudentId} not found.");
			return _mapper.Map<StudentResponseDto>(student);
		}
	}
}
