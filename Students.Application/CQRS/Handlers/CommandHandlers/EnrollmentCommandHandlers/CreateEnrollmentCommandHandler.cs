using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Hybrid;
using Students.Application.Contracts;
using Students.Application.CQRS.Commands.EnrollmentCommands;
using Students.Application.Responses;
using Students.Application.Validators.DtoCreateValidators;
using Students.Domain.Entities;

namespace Students.Application.CQRS.Handlers.CommandHandlers.EnrollmentCommandHandlers
{
	public class CreateEnrollmentCommandHandler(IEnrollmentRepository enrollmentRepository, IMapper mapper) 
		: IRequestHandler<CreateEnrollmentCommand, BaseCommandResponse>
	{
		private readonly IEnrollmentRepository _enrollmentRepository = enrollmentRepository;
		private readonly IMapper _mapper = mapper;

		public async Task<BaseCommandResponse> Handle(CreateEnrollmentCommand request, CancellationToken cancellationToken)
		{
			var response = new BaseCommandResponse();
			var enrollmentValidator = new EnrollmentCreateDtoValidator();
			var validationResult = await enrollmentValidator.ValidateAsync(request.Enrollment!, cancellationToken);

			if (!validationResult.IsValid)
			{
				response.IsSuccess = false;
				response.Message = "Enrollment creation unsuccessful.";
				response.Errors = [.. validationResult.Errors.Select(e => e.ErrorMessage)];
				return response;
			}

			var enrollment = _mapper.Map<Enrollment>(request.Enrollment);
			await _enrollmentRepository.AddAsync(enrollment, cancellationToken);
			
			response.IsSuccess = true;
			response.Message = "Enrollment creation successful.";
			response.Id = enrollment.EnrollmentId;
			return response;
		}
	}
}
