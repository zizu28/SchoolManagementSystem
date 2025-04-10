using Courses.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations
{
	public class CourseConfiguration : IEntityTypeConfiguration<Course>
	{
		public void Configure(EntityTypeBuilder<Course> builder)
		{
			builder.HasKey(c => c.CourseId);
			builder.Property(c => c.CourseId)
				.ValueGeneratedOnAdd()
				.IsRequired();
			builder.Property(c => c.Title)
				.HasMaxLength(100)
				.IsRequired();
			builder.Property(c => c.Description)
				.HasMaxLength(500)
				.IsRequired();
			builder.Property(c => c.Credits)
				.IsRequired();
			builder.Property(c => c.Department)
				.HasMaxLength(50)
				.IsRequired();
			builder.Property(c => c.Instructor)
				.HasMaxLength(100)
				.IsRequired();
		}
	}
}
