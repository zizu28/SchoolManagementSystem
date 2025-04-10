using Courses.Domain.Entities;
using Identity.API.Models;
using Infrastructure.Database.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Students.Domain.Entities;


namespace Infrastructure.Database
{
	public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
		: IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>(options)
	{
		public DbSet<Student> Students { get; set; }
		public DbSet<Enrollment.Domain.Entities.Enrollment> Enrollments { get; set; }
		public DbSet<Course> Courses { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.ApplyConfiguration(new StudentConfiguration());
			builder.ApplyConfiguration(new EnrollmentConfiguration());
			builder.ApplyConfiguration(new CourseConfiguration());
		}
	}
}
