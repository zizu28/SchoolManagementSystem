namespace Core.EventBus
{
	public class IntegrationEvent
	{
		public Guid Id { get; }
		public DateTime CreatedAt { get; }
		public IntegrationEvent()
		{
			Id = Guid.NewGuid();
			CreatedAt = DateTime.UtcNow;
		}
	}
}
