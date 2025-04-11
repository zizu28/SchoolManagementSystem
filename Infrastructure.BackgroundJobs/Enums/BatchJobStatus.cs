namespace Infrastructure.BackgroundJobs.Enums
{
	public enum BatchJobStatus
	{
		Pending,
		InProgress,
		Completed,
		PartiallyFailed,
		Failed
	}
}
