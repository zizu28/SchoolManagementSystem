using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Hybrid;
using Students.Application.Contracts;
using Students.Application.CQRS.Commands.AdmissionApplicationCommands;
using Students.Application.Responses;
using Students.Application.Validators.DtoCreateValidators;
using Students.Domain.Entities;

namespace Students.Application.CQRS.Handlers.CommandHandlers.AdmissionApplicationCommandHandlers
{
	public class CreateAdmissionApplicationCommandHandler(IAdmissionApplicationRepository repository, IMapper mapper) 
		: IRequestHandler<CreateAdmissionApplicationCommand, BaseCommandResponse>
	{
		private readonly IAdmissionApplicationRepository _repository = repository;
		private readonly IMapper _mapper = mapper;

		public async Task<BaseCommandResponse> Handle(CreateAdmissionApplicationCommand request, CancellationToken cancellationToken)
		{
			var response = new BaseCommandResponse();
			var admissionApplicationValidator = new AdmissionApplicationCreateDtoValidator();
			var validationResult = await admissionApplicationValidator.ValidateAsync(request.AdmissionApplication!, cancellationToken);

			if (!validationResult.IsValid)
			{
				response.IsSuccess = false;
				response.Message = "Admission application creation unsuccessful.";
				response.Errors = [.. validationResult.Errors.Select(e => e.ErrorMessage)];
				return response;
			}

			var academicAdmission = _mapper.Map<AdmissionApplication>(request.AdmissionApplication);
			await _repository.AddAsync(academicAdmission, cancellationToken);

			response.IsSuccess = true;
			response.Message = "Admission application creation successful.";
			response.Id = academicAdmission.Id;
			return response;
		}
	}
}
