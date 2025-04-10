namespace Departments.Domain.Entities
{
	public class Department
	{
		public Guid DepartmentId { get; set; }
		public string Name { get; set; } = string.Empty;
		public ICollection<Course> Courses { get; set; } = [];
	}
}
