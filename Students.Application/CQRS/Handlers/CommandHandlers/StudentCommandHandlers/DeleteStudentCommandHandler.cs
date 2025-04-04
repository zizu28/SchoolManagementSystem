using MediatR;
using Microsoft.Extensions.Caching.Hybrid;
using Students.Application.Contracts;
using Students.Application.CQRS.Commands.StudentCommands;
using Students.Application.Exceptions;

namespace Students.Application.CQRS.Handlers.CommandHandlers.StudentCommandHandlers
{
	public class DeleteStudentCommandHandler(IStudentRepository studentRepository, HybridCache cache) 
		: IRequestHandler<DeleteStudentCommand, Unit>
	{
		private readonly IStudentRepository _studentRepository = studentRepository;
		private readonly HybridCache _cache = cache;

		public async Task<Unit> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
		{
			var student = await _studentRepository.GetByIdAsync(request.StudentId, cancellationToken)
				?? throw new NotFoundException($"Student with id {request.StudentId} not found.");

			await _cache.RemoveAsync($"student-{student.StudentId}", cancellationToken);
			await _studentRepository.DeleteAsync(student, cancellationToken);
			return Unit.Value;
		}
	}
}
