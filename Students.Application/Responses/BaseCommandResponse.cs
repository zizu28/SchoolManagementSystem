namespace Students.Application.Responses
{
	public class BaseCommandResponse
	{
		public Guid Id { get; set; }
		public string? Message { get; set; }
		public bool IsSuccess { get; set; } = false;
		public List<string> Errors { get; set; } = [];
	}
}
