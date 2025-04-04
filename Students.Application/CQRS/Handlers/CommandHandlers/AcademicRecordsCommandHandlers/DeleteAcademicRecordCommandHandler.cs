﻿using MediatR;
using Microsoft.Extensions.Caching.Hybrid;
using Students.Application.Contracts;
using Students.Application.CQRS.Commands.AcademicRecordsCommands;
using Students.Application.Exceptions;

namespace Students.Application.CQRS.Handlers.CommandHandlers.AcademicRecordsCommandHandlers
{
	public class DeleteAcademicRecordCommandHandler(HybridCache cache, IAcademicRecordRepository recordRepository) 
		: IRequestHandler<DeleteAcademicRecordCommand, Unit>
	{
		private readonly HybridCache _cache = cache ?? throw new ArgumentNullException(nameof(cache));
		private readonly IAcademicRecordRepository _recordRepository = recordRepository ?? throw new ArgumentNullException(nameof(recordRepository));

		public async Task<Unit> Handle(DeleteAcademicRecordCommand request, CancellationToken cancellationToken)
		{
			var academicRecord = await _recordRepository.GetByIdAsync(request.AcademicRecordId, cancellationToken)
				?? throw new NotFoundException($"Academic record with id {request.AcademicRecordId} not found.");

			await _cache.RemoveAsync($"academic-record-{academicRecord.AcademicRecordId}", cancellationToken);
			await _recordRepository.DeleteAsync(academicRecord, cancellationToken);
			
			return Unit.Value;
		}
	}
}
