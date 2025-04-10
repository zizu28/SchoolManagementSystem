using System.Linq.Expressions;

namespace Infrastructure.BackgroundJobs.Abstractions
{
	public interface IBackgroundJobService
	{
		// Enqueue a fire-and-forget job
		string Enqueue(Expression<Func<Task>> methodCall);
		string Enqueue<T>(Expression<Func<T, Task>> methodCall);


		// Schedule a job to be executed at a specific time
		string Schedule(Expression<Func<Task>> methodCall, DateTimeOffset scheduledTime);
		string Schedule<T>(Expression<Func<T, Task>> methodCall, DateTimeOffset scheduledTime);


		// Schedule a recurring job
		void AddOrUpdateRecurringJob(string jobId, Expression<Func<Task>> methodCall, Func<string> cronExpression);
		void RemoveRecurringJob(string jobId);


		// Continuation jobs
		string ContinueJobWith(string parentJobId, Expression<Func<Task>> methodCall);
		IEnumerable<string> CreateBatch(Action<IBatchJobBuilder> batchBuilder);
	}
}
