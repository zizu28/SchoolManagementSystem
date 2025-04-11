using Infrastructure.BackgroundJobs.Enums;

namespace Infrastructure.BackgroundJobs.Models
{
	public class BatchJobInfo
	{
		public string BatchId { get; set; } = Guid.NewGuid().ToString();
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public List<string> JobIds { get; set; } = [];
		public BatchJobStatus Status { get; set; } = BatchJobStatus.Pending;
	}
}
