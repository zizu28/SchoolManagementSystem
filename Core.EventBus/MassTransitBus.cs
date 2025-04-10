using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Core.EventBus
{
	public static class MassTransitBus
	{
		public static IServiceCollection AddMassTransitWithRabbitMQ(this IServiceCollection services,
			IConfiguration configuration)
		{
			services.AddMassTransit(busConfig =>
			{
				busConfig.AddConsumers(Assembly.GetEntryAssembly());
				busConfig.UsingRabbitMq((context, config) =>
				{
					config.Host(configuration["RabbitMQ:HostName"], "/", x =>
					{
						x.Username(configuration["RabbitMQ:UserName"]!);
						x.Password(configuration["RabbitMQ:Password"]!);
					});

					config.ConfigureEndpoints(context);
				});
			});

			services.AddOptions<MassTransitHostOptions>()
				.Configure(options =>
				{
					options.WaitUntilStarted = true;
					options.StartTimeout = TimeSpan.FromSeconds(30);
					options.StopTimeout = TimeSpan.FromMinutes(1);
				});

			return services;
		}
	}
}
