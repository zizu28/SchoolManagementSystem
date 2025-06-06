﻿using Microsoft.EntityFrameworkCore;
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

			modelBuilder.Entity<AcademicRecord>()
				.HasOne(s => s.Student)
				.WithMany(s => s.AcademicRecords)
				.HasForeignKey(s => s.StudentId);

			modelBuilder.Entity<Student>().HasIndex(s => s.Email).IsUnique();

			modelBuilder.Entity<Student>().HasIndex(s => s.FirstName);
			modelBuilder.Entity<Student>().HasIndex(s => s.LastName);

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

			// Academic record configuration
			modelBuilder.Entity<AcademicRecord>()
				.Property(a => a.GPA)
				.HasPrecision(3, 2);
		}
	}
}