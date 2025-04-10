using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Students.Domain.Entities;
using Students.Domain.Enums;

namespace Infrastructure.Database.Configurations
{
	public class StudentConfiguration : IEntityTypeConfiguration<Student>
	{
		public void Configure(EntityTypeBuilder<Student> builder)
		{
			builder.HasKey(s => s.StudentId);
			builder.Property(s => s.FirstName)
				.IsRequired()
				.HasMaxLength(100);
			builder.Property(s => s.LastName)
				.IsRequired()
				.HasMaxLength(100);
			builder.Property(s => s.Email)
				.IsRequired()
				.HasMaxLength(256);
			builder.Property(s => s.PhoneNumber)
				.HasMaxLength(20);
			builder.Property(s => s.Address)
				.HasMaxLength(256);
			builder.Property(s => s.EnrollmentDate)
				.HasColumnType("datetime2");
			builder.Property(s => s.Major)
				.HasMaxLength(100);
			builder.Property(s => s.Minor)
				.HasMaxLength(100);
			builder.Property(s => s.StudentStatus)
				.HasConversion(v => v.ToString(), v => Enum.Parse<StudentStatus>(v))
				.HasMaxLength(20);
		}
	}
}
