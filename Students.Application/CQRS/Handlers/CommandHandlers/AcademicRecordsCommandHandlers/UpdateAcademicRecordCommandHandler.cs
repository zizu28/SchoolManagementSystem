using AutoMapper;
using MediatR;
using Students.Application.Contracts;
using Students.Application.CQRS.Commands.AcademicRecordsCommands;
using Students.Application.Exceptions;
using Students.Application.Responses;
using Students.Application.Validators.DtoUpdateValidators;

namespace Students.Application.CQRS.Handlers.CommandHandlers.AcademicRecordsCommandHandlers
{
	public class UpdateAcademicRecordCommandHandler(IAcademicRecordRepository recordRepository, IMapper mapper)
		: IRequestHandler<UpdateAcademicRecordCommand, BaseCommandResponse>
	{
		private readonly IAcademicRecordRepository _recordRepository = recordRepository;
		private readonly IMapper _mapper = mapper;

		public async Task<BaseCommandResponse> Handle(UpdateAcademicRecordCommand request, CancellationToken cancellationToken)
		{
			var response = new BaseCommandResponse();
			var academicRecordValidator = new AcademicRecordUpdateDtoValidator();
			var validationResult = await academicRecordValidator.ValidateAsync(request.AcademicRecord!, cancellationToken);

			if (!validationResult.IsValid)
			{
				response.IsSuccess = false;
				response.Message = "Academic record update unsuccessful.";
				response.Errors = [.. validationResult.Errors.Select(e => e.ErrorMessage)];
				return response;
			}

			var academicRecord = await _recordRepository.GetByIdAsync(request.AcademicRecordId, cancellationToken)
				?? throw new NotFoundException($"Academic record with id {request.AcademicRecordId} not found.");
			_mapper.Map(request.AcademicRecord, academicRecord);
			await _recordRepository.UpdateAsync(academicRecord, cancellationToken);

			response.IsSuccess = true;
			response.Message = "Academic record update successful.";
			response.Id = academicRecord.AcademicRecordId;
			return response;
		}
	}
}
