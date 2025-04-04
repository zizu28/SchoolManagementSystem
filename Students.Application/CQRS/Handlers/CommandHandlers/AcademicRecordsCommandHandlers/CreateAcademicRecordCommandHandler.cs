using AutoMapper;
using MediatR;
using Students.Application.Contracts;
using Students.Application.CQRS.Commands.AcademicRecordsCommands;
using Students.Application.Responses;
using Students.Application.Validators.DtoCreateValidators;
using Students.Domain.Entities;

namespace Students.Application.CQRS.Handlers.CommandHandlers.AcademicRecordsCommandHandlers
{
	public class CreateAcademicRecordCommandHandler(IAcademicRecordRepository recordRepository, IMapper mapper) 
		: IRequestHandler<CreateAcademicRecordCommand, BaseCommandResponse>
	{
		private readonly IAcademicRecordRepository _recordRepository = recordRepository;
		private readonly IMapper _mapper = mapper;

		public async Task<BaseCommandResponse> Handle(CreateAcademicRecordCommand request, CancellationToken cancellationToken)
		{
			var response = new BaseCommandResponse();
			var academicRecordValidator = new AcademicRecordCreateDtoValidator();
			var validationResult = await academicRecordValidator.ValidateAsync(request.AcademicRecord!, cancellationToken);

			if (!validationResult.IsValid)
			{
				response.IsSuccess = false;
				response.Message = "Academic record creation unsuccessful.";
				response.Errors = [.. validationResult.Errors.Select(e => e.ErrorMessage)];
				return response;
			}

			var academicRecord = _mapper.Map<AcademicRecord>(request.AcademicRecord);
			await _recordRepository.AddAsync(academicRecord, cancellationToken);
			
			response.IsSuccess = true;
			response.Message = "Academic record creation successful.";
			response.Id = academicRecord.AcademicRecordId;
			return response;
		}
	}
}
