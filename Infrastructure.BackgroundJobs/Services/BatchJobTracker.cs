using Infrastructure.BackgroundJobs.Enums;
using Infrastructure.BackgroundJobs.Models;
using System.Collections.Concurrent;

namespace Infrastructure.BackgroundJobs.Services
{
	public class BatchJobTracker
	{
		private readonly ConcurrentDictionary<string, BatchJobInfo> _jobs = new();

		public BatchJobInfo CreateBatchJob(IEnumerable<string> jobIds)
		{
			var batchJob = new BatchJobInfo { JobIds = [.. jobIds] };
			_jobs[batchJob.BatchId] = batchJob;
			return batchJob;
		}

		public void UpdateBatchJobStatus(string batchId, BatchJobStatus status)
		{
			if(_jobs.TryGetValue(batchId, out var batchJob))
			{
				batchJob.Status = status;
			}
		}

		public BatchJobInfo GetBatchJobInfo(string batchId)
		{
			var result = _jobs.TryGetValue(batchId, out var batchJob) ? batchJob : null;
			return result!;
		}
	}
}
