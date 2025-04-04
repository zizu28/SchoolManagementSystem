using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Students.Infrastructure.Data
{
	public class StudentDbContextFactory : IDesignTimeDbContextFactory<StudentDbContext>
	{
		public StudentDbContext CreateDbContext(string[] args)
		{
			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();

			var optionsBuilder = new DbContextOptionsBuilder<StudentDbContext>();
			var connectionString = configuration.GetConnectionString("DefaultConnection");
			optionsBuilder.UseSqlServer(connectionString);

			return new StudentDbContext(optionsBuilder.Options);
		}
	}
}
