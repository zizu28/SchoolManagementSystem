using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace Students.Domain.Entities
{
	[NotMapped]
	public class ErrorDetails
	{
		public string? Message { get; set; }
		public int StatusCode { get; set; }

		public override string ToString() => JsonSerializer.Serialize(this);
	}
}
