using Core.Identity.Models;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Students.Application.Events;

namespace Identity.API.Consumers
{
	public class StudentCreatedIntegrationEventConsumer(UserManager<ApplicationUser> userManager, 
		ILogger<StudentCreatedIntegrationEventConsumer> logger)
		: IConsumer<StudentCreatedIntegrationEvent>
	{
		private readonly UserManager<ApplicationUser> _userManager = userManager;
		private readonly ILogger<StudentCreatedIntegrationEventConsumer> _logger = logger;

		public async Task Consume(ConsumeContext<StudentCreatedIntegrationEvent> context)
		{
			var @event = context.Message;
			_logger.LogInformation($"Received StudentCreatedIntegrationEvent with id: {@event.Id}");

			var user = new ApplicationUser { UserName = @event.Email, Email = @event.Email, Id = @event.Id };
			var result = await _userManager.CreateAsync(user);

			if(result.Succeeded)
			{
				_logger.LogInformation($"User created successfully with id: {user.Id}");
			}
			else
			{
				_logger.LogError($"Failed to create user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
			}
			await Task.CompletedTask;
		}
	}
}
