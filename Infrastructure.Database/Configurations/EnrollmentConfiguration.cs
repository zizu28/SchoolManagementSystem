using Enrollment.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations
{
	public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment.Domain.Entities.Enrollment>
	{
		public void Configure(EntityTypeBuilder<Enrollment.Domain.Entities.Enrollment> builder)
		{
			builder.HasKey(e => e.EnrollmentId);
			builder.Property(e => e.EnrollmentDate)
				.HasColumnType("datetime2")
				.IsRequired();
			builder.Property(e => e.Grade)
				.HasMaxLength(5);
			builder.Property(e => e.EnrollmentStatus)
				.HasConversion(v => v.ToString(), v => Enum.Parse<EnrollmentStatus>(v))
				.HasMaxLength(20);
			builder.Property(e => e.StudentId)
				.IsRequired();
			builder.Property(e => e.CourseId)
				.IsRequired();
		}
	}
}
