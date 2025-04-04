namespace SchoolManagement.Core.Entities
{
	public abstract class EntityBase
	{
		public Guid Id { get; protected set; } = Guid.NewGuid();
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public DateTime UpdatedAt { get; set; } 
	}
}
