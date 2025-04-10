using System.Linq.Expressions;

namespace Infrastructure.BackgroundJobs.Abstractions
{
	public interface IBatchJobBuilder
	{
		void AddJob(Expression<Func<Task>> methodCall);
		void AddContinuation(Expression<Func<Task>> methodCall, string dependentJobId);
	}
}
