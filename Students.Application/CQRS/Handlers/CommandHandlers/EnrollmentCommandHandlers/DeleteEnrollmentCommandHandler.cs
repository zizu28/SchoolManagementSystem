using MediatR;
using Microsoft.Extensions.Caching.Hybrid;
using Students.Application.Contracts;
using Students.Application.CQRS.Commands.EnrollmentCommands;
using Students.Application.Exceptions;

namespace Students.Application.CQRS.Handlers.CommandHandlers.EnrollmentCommandHandlers
{
	public class DeleteEnrollmentCommandHandler(IEnrollmentRepository enrollmentRepository)
		: IRequestHandler<DeleteEnrollmentCommand, Unit>
	{
		private readonly IEnrollmentRepository _enrollmentRepository = enrollmentRepository;

		public async Task<Unit> Handle(DeleteEnrollmentCommand request, CancellationToken cancellationToken)
		{
			var enrollment = await _enrollmentRepository.GetByIdAsync(request.EnrollmentId, cancellationToken)
				?? throw new NotFoundException($"Enrollment with id {request.EnrollmentId} not found.");
			await _enrollmentRepository.DeleteAsync(enrollment, cancellationToken);

			return Unit.Value;
		}
	}
}
