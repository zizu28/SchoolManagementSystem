using AutoMapper;
using MediatR;
using Students.Application.Contracts;
using Students.Application.CQRS.Commands.EnrollmentCommands;
using Students.Application.Exceptions;
using Students.Application.Responses;
using Students.Application.Validators.DtoUpdateValidators;

namespace Students.Application.CQRS.Handlers.CommandHandlers.EnrollmentCommandHandlers
{
	public class UpdateEnrollmentCommandHandler(IEnrollmentRepository enrollmentRepository, IMapper mapper)
		: IRequestHandler<UpdateEnrollmentCommand, BaseCommandResponse>
	{
		private readonly IEnrollmentRepository _enrollmentRepository = enrollmentRepository;
		private readonly IMapper _mapper = mapper;

		public async Task<BaseCommandResponse> Handle(UpdateEnrollmentCommand request, CancellationToken cancellationToken)
		{
			var response = new BaseCommandResponse();
			var enrollmentValidator = new EnrollmentUpdateDtoValidator();
			var validationResult = await enrollmentValidator.ValidateAsync(request.Enrollment!, cancellationToken);

			if (!validationResult.IsValid)
			{
				response.IsSuccess = false;
				response.Message = "Enrollment update unsuccessful.";
				response.Errors = [.. validationResult.Errors.Select(e => e.ErrorMessage)];
				return response;
			}

			var enrollment = await _enrollmentRepository.GetByIdAsync(request.EnrollmentId, cancellationToken)
				?? throw new NotFoundException($"Enrollment with id {request.EnrollmentId} not found.");
			_mapper.Map(request.Enrollment, enrollment);
			await _enrollmentRepository.UpdateAsync(enrollment, cancellationToken);

			response.IsSuccess = true;
			response.Message = "Enrollment update successful.";
			response.Id = enrollment.EnrollmentId;
			return response;
		}
	}
}
