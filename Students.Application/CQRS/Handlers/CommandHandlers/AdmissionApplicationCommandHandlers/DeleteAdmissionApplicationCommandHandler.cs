using MediatR;
using Microsoft.Extensions.Caching.Hybrid;
using Students.Application.Contracts;
using Students.Application.CQRS.Commands.AdmissionApplicationCommands;
using Students.Application.Exceptions;

namespace Students.Application.CQRS.Handlers.CommandHandlers.AdmissionApplicationCommandHandlers
{
	public class DeleteAdmissionApplicationCommandHandler(HybridCache cache, IAdmissionApplicationRepository repository) 
		: IRequestHandler<DeleteAdmissionApplicationCommand, Unit>
	{
		private readonly HybridCache _cache = cache;
		private readonly IAdmissionApplicationRepository _repository = repository;

		public async Task<Unit> Handle(DeleteAdmissionApplicationCommand request, CancellationToken cancellationToken)
		{
			var admissionApplication = await _repository.GetByIdAsync(request.AdmissionApplicationId, cancellationToken)
				?? throw new NotFoundException($"Admission application with id {request.AdmissionApplicationId} not found.");
			await _cache.RemoveAsync($"admission-application-{admissionApplication.Id}", cancellationToken);
			await _repository.DeleteAsync(admissionApplication, cancellationToken);

			return Unit.Value;
		}
	}
}
