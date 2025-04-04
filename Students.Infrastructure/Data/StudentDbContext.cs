using Microsoft.EntityFrameworkCore;
using Students.Domain.Entities;
using Students.Domain.Enums;

namespace Students.Infrastructure.Data
{
	public class StudentDbContext : DbContext
	{
		public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
		{
		}

		public DbSet<Student> Students { get; set; }
		public DbSet<Enrollment> Enrollments { get; set; }
		public DbSet<AcademicRecord> AcademicRecords { get; set; }
		public DbSet<AdmissionApplication> AdmissionApplications { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Student configuration
			modelBuilder.Entity<Student>()
				.HasMany(s => s.Applications)
				.WithOne(a => a.Student)
				.HasForeignKey(a => a.StudentId);

			modelBuilder.Entity<Student>()
				.HasMany(s => s.Enrollments)
				.WithOne(e => e.Student)
				.HasForeignKey(e => e.StudentId);

			modelBuilder.Entity<Student>()
				.HasOne(s => s.AcademicRecord)
				.WithOne(a => a.Student)
				.HasForeignKey<AcademicRecord>(a => a.StudentId);


			modelBuilder.Entity<Student>().HasIndex(s => s.Email).IsUnique();

			// Enrollments configuration
			modelBuilder.Entity<Enrollment>()
				.Property(e => e.Status).HasConversion(
				v => v.ToString(), v => Enum.Parse<EnrollmentStatus>(v));

			// Admission application configuration
			modelBuilder.Entity<AdmissionApplication>()
				.Property(aa => aa.Status).HasConversion(
				v => v.ToString(), v => Enum.Parse<AdmissionStatus>(v));

			modelBuilder.Entity<AdmissionApplication>()
				.HasIndex(aa => aa.ProgramCode).IsUnique();
		}
	}
}