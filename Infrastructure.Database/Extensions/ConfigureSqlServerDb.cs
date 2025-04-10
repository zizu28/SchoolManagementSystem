using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Database.Extensions
{
	public static class ConfigureSqlServerDb
	{
		public static IServiceCollection AddSqlServerConfiguration(this IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString("DefaultConnection");
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(connectionString));
			return services;
		}
	}
}
