using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Events;
using Serilog;
using Students.Application.Contracts;
using Students.Infrastructure.Data;
using Students.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Hybrid;
using Students.Infrastructure.CacheServices.AcademicRecordCache;
using Students.Infrastructure.CacheServices.StudentCache;
using Students.Infrastructure.CacheServices.EnrollmentCache;
using Students.Infrastructure.CacheServices.AdmissionApplicationCache;

namespace Students.Infrastructure
{
	public static class InfrastructureServiceRegistration
	{
		public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString("DefaultConnection");
			services.AddDbContext<StudentDbContext>(options =>
			{
				options.UseSqlServer(connectionString);
			});

			// Configure the Hybrid Cache
			services.AddHybridCache(opt =>
			{
				opt.DefaultEntryOptions = new HybridCacheEntryOptions
				{
					Expiration = TimeSpan.FromMinutes(10),
					LocalCacheExpiration = TimeSpan.FromMinutes(5)
				};
			});

			services.AddStackExchangeRedisCache(opt =>
			{
				opt.Configuration = configuration.GetConnectionString("Redis");
				opt.InstanceName = "StudentManagementService";
			});

			// Register repository definitions and implementations
			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			services.AddScoped<IStudentRepository, StudentRepository>();
			services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
			services.AddScoped<IAcademicRecordRepository, AcademicReportRepository>();
			services.AddScoped<IAdmissionApplicationRepository, AdmissionApplicationRepository>();

			// Register cache definitions and implementations
			services.AddScoped<IStudentCacheService, StudentCacheService>();
			services.AddScoped<IEnrollmentCacheSevice, EnrollmentCacheService>();
			services.AddScoped<IAcademicRecordCacheService, AcademicRecordCacheService>();
			services.AddScoped<IAdmissionApplicationCache, AdmissionApplicationCacheService>();

			return services;
		}

		public static WebApplicationBuilder SerilogConfiguration(this WebApplicationBuilder builder)
		{
			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Debug()
				.MinimumLevel.Override("Microsoft", LogEventLevel.Information)
				.WriteTo.Console()
				.WriteTo.File("StudentManagementLogs/log-.txt", rollingInterval: RollingInterval.Day)
				.Enrich.FromLogContext()
				.Enrich.WithProperty("Application", "StudentManagementService")
				.CreateLogger();

			builder.Logging.ClearProviders();
			builder.Host.UseSerilog();
			return builder;
		}
	}
}