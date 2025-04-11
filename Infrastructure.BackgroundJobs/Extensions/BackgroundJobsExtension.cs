using Hangfire;
using Hangfire.Annotations;
using Hangfire.Dashboard;
using Hangfire.SqlServer;
using Infrastructure.BackgroundJobs.Abstractions;
using Infrastructure.BackgroundJobs.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.BackgroundJobs.Extensions
{
	public static class BackgroundJobsExtension
	{
		public static IServiceCollection AddBackgroundJobs(
			this IServiceCollection services, IConfiguration configuration)
		{
			services.AddHangfire(config =>
			{
				config.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
				.UseRecommendedSerializerSettings()
				.UseColouredConsoleLogProvider()
				.UseSimpleAssemblyNameTypeSerializer()
				.UseRecommendedSerializerSettings()
				.UseSqlServerStorage(configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
				{
					CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
					SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
					QueuePollInterval = TimeSpan.Zero,
					DisableGlobalLocks = true,
					UseRecommendedIsolationLevel = true
				});
			});

			services.AddHangfireServer(options =>
			{
				options.WorkerCount = Environment.ProcessorCount * 5;
				options.Queues = ["default", "critical"];
			});

			services.AddScoped<IBackgroundJobService, HangfireBackgroundJobService>();
			services.AddSingleton<BatchJobTracker>();

			return services;
		}

		public static IApplicationBuilder UseHangfireDashboard(this IApplicationBuilder app)
		{
			app.UseHangfireDashboard("/hangfire", new DashboardOptions
			{
				Authorization = [ new HangfireDashboardAuthorizationFilter() ],
			});

			return app;
		}
	}

	public class HangfireDashboardAuthorizationFilter : IDashboardAuthorizationFilter
	{
		public bool Authorize([NotNull] DashboardContext context)
		{
			if (context.GetHttpContext().User.Identity!.IsAuthenticated)
			{
				return true;
			}

			// Allow all authenticated users to see the Dashboard (potentially dangerous).
			// You can specify your own authorization logic here.
			// For example, you can check if the user is in a specific role or has a specific claim.
			if (context.GetHttpContext().User.IsInRole("Admin"))
			{
				return true;
			}

			// Redirect to login page if not authorized
			context.GetHttpContext().Response.Redirect("/Account/Login");
			return false;
		}
	}
}
