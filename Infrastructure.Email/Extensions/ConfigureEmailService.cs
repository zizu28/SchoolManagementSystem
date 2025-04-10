using Infrastructure.Email.Models;
using Infrastructure.Email.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Email.Extensions
{
	public static class ConfigureEmailService
	{
		public static IServiceCollection AddEmailService(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<EmailSettings>(options =>
			{
				configuration.GetSection("EmailSettings");
			});
			services.AddScoped<IEmailService, FluentEmailService>();

			return services;
		}
	}
}
