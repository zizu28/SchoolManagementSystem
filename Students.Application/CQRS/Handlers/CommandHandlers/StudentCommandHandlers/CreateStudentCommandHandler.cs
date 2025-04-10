using AutoMapper;
using MediatR;
using Students.Application.Contracts;
using Students.Application.CQRS.Commands.StudentCommands;
using Students.Application.Events;
using Students.Application.Responses;
using Students.Application.Validators.DtoCreateValidators;
using Students.Domain.Entities;

namespace Students.Application.CQRS.Handlers.CommandHandlers.StudentCommandHandlers
{
	public class CreateStudentCommandHandler(IStudentRepository studentRepository, 
		IMapper mapper, IPublisher publisher) 
		: IRequestHandler<CreateStudentCommand, BaseCommandResponse>
	{
		private readonly IStudentRepository _studentRepository = studentRepository;
		private readonly IMapper _mapper = mapper;
		private readonly IPublisher _publisher = publisher;

		public async Task<BaseCommandResponse> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
		{
			var response = new BaseCommandResponse();
			var studentValidator = new StudentCreateDtoValidator();
			var validationResult = await studentValidator.ValidateAsync(request.Student!, cancellationToken);
			
			if (!validationResult.IsValid)
			{
				response.IsSuccess = false;
				response.Message = "Student creation unsuccessful.";
				response.Errors = [.. validationResult.Errors.Select(e => e.ErrorMessage)];
				return response;
			}

			var student = _mapper.Map<Student>(request.Student);
			
			await _studentRepository.AddAsync(student, cancellationToken);

			response.IsSuccess = true;
			response.Message = "Student added successfully.";
			response.Id = student.StudentId;

			await _publisher.Publish(new StudentCreatedEvent(student.StudentId, student.Email), cancellationToken);
			
			return response;
		}
	}
}
