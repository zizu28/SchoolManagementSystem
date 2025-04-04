using AutoMapper;
using MediatR;
using Students.Application.Contracts;
using Students.Application.CQRS.Commands.AdmissionApplicationCommands;
using Students.Application.Exceptions;
using Students.Application.Responses;
using Students.Application.Validators.DtoUpdateValidators;

namespace Students.Application.CQRS.Handlers.CommandHandlers.AdmissionApplicationCommandHandlers
{
	public class UpdateAdmissionApplicationCommandHandler(IAdmissionApplicationRepository repository, IMapper mapper) 
		: IRequestHandler<UpdateAdmissionApplicationCommand, BaseCommandResponse>
	{
		private readonly IAdmissionApplicationRepository _repository = repository;
		private readonly IMapper _mapper = mapper;

		public async Task<BaseCommandResponse> Handle(UpdateAdmissionApplicationCommand request, CancellationToken cancellationToken)
		{
			var response = new BaseCommandResponse();
			var admissionApplicationValidator = new AdmissionApplicationUpdateDtoValidator();
			var validationResult = await admissionApplicationValidator.ValidateAsync(request.AdmissionApplication!, cancellationToken);

			if (!validationResult.IsValid)
			{
				response.IsSuccess = false;
				response.Message = "Admission application update unsuccessful.";
				response.Errors = [.. validationResult.Errors.Select(e => e.ErrorMessage)];
				return response;
			}

			var admissionApplication = await _repository.GetByIdAsync(request.AdmissionApplicationId, cancellationToken)
				?? throw new NotFoundException($"Admission application with id {request.AdmissionApplicationId} not found.");

			_mapper.Map(request.AdmissionApplication, admissionApplication);
			await _repository.UpdateAsync(admissionApplication, cancellationToken);
			response.IsSuccess = true;
			response.Message = "Admission application update successful.";
			response.Id = admissionApplication.Id;
			return response;
		}
	}
}
