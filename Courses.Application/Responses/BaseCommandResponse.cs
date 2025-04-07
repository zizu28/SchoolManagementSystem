﻿namespace Courses.Application.Responses
{
	public class BaseCommandResponse
	{
		public bool IsSuccess { get; set; }
		public string? Message { get; set; }
		public List<string> Errors { get; set; } = [];
	}
}
