using AutoMapper;
using MediatR;
using Students.Application.Contracts;
using Students.Application.CQRS.Commands.StudentCommands;
using Students.Application.Exceptions;
using Students.Application.Responses;
using Students.Application.Validators.DtoUpdateValidators;

namespace Students.Application.CQRS.Handlers.CommandHandlers.StudentCommandHandlers
{
	public class UpdateStudentCommandHandler(IStudentRepository studentRepository, IMapper mapper) 
		: IRequestHandler<UpdateStudentCommand, BaseCommandResponse>
	{
		private readonly IStudentRepository _studentRepository = studentRepository;
		private readonly IMapper _mapper = mapper;

		public async Task<BaseCommandResponse> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
		{
			var response = new BaseCommandResponse();
			var studentValidator = new StudentUpdateDtoValidator();
			var validationResult = await studentValidator.ValidateAsync(request.Student!, cancellationToken);

			if (!validationResult.IsValid)
			{
				response.IsSuccess = false;
				response.Message = "Student update unsuccessful.";
				response.Errors = [.. validationResult.Errors.Select(e => e.ErrorMessage)];
				return response;
			}

			var student = await _studentRepository.GetByIdAsync(request.StudentId, cancellationToken)
				?? throw new NotFoundException($"Student with id {request.StudentId} not found.");
			_mapper.Map(request.Student, student);
			await _studentRepository.UpdateAsync(student, cancellationToken);

			response.IsSuccess = true;
			response.Message = "Student updated successfully.";
			response.Id = student.StudentId;
			return response;
		}
	}
}
