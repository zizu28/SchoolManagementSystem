namespace Staffs.Domain.Entities
{
	public class Staff
	{
		public Guid StaffId { get; set; }
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
	}
}
