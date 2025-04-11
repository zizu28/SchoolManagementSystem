using Hangfire;
using Infrastructure.BackgroundJobs.Abstractions;
using System.Linq.Expressions;

namespace Infrastructure.BackgroundJobs.Services
{
	public class HangfireBackgroundJobService(BatchJobTracker jobTracker) : IBackgroundJobService
	{
		private readonly BatchJobTracker _jobTracker = jobTracker;

		public void AddOrUpdateRecurringJob(string jobId, Expression<Func<Task>> methodCall, Func<string> cronExpression)
		{
			RecurringJob.AddOrUpdate(jobId, methodCall, cronExpression());
		}

		public string ContinueJobWith(string parentJobId, Expression<Func<Task>> methodCall)
		{
			return BackgroundJob.ContinueJobWith(parentJobId, methodCall);
		}

		public IEnumerable<string> CreateBatch(Action<IBatchJobBuilder> batchBuilder)
		{
			var builder = new HangfireBatchJobBuilder();
			batchBuilder(builder);
			var jobIds = builder.Execute();

			_jobTracker.CreateBatchJob(jobIds);

			return jobIds;
		}

		internal class HangfireBatchJobBuilder : IBatchJobBuilder
		{
			private readonly List<Expression<Func<Task>>> _jobs = [];
			private readonly List<(Expression<Func<Task>> job, string DependentJobId)> _continuations = [];
			public void AddContinuation(Expression<Func<Task>> methodCall, string dependentJobId)
			{
				_continuations.Add((methodCall, dependentJobId));
			}

			public void AddJob(Expression<Func<Task>> methodCall)
			{
				_jobs.Add(methodCall);
			}

			public IEnumerable<string> Execute()
			{
				//Execute initial jobs
				var jobIds = _jobs.Select(j => BackgroundJob.Enqueue(j)).ToList();

				// Add continuations
				foreach(var (continuationJob, dependentJobId) in _continuations)
				{
					BackgroundJob.ContinueJobWith(dependentJobId, continuationJob);
				}

				return jobIds;
			}
		}
		public string Enqueue(Expression<Func<Task>> methodCall)
		{
			return BackgroundJob.Enqueue(methodCall);
		}

		public string Enqueue<T>(Expression<Func<T, Task>> methodCall)
		{
			return BackgroundJob.Enqueue(methodCall);
		}

		public void RemoveRecurringJob(string jobId)
		{
			RecurringJob.RemoveIfExists(jobId);
		}

		public string Schedule(Expression<Func<Task>> methodCall, DateTimeOffset scheduledTime)
		{
			return BackgroundJob.Schedule(methodCall, scheduledTime);
		}

		public string Schedule<T>(Expression<Func<T, Task>> methodCall, DateTimeOffset scheduledTime)
		{
			return BackgroundJob.Schedule(methodCall, scheduledTime);
		}
	}
}
