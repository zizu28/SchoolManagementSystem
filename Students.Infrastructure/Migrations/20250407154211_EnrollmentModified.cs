using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Students.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EnrollmentModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CourseSectionId",
                table: "Enrollments",
                newName: "CourseOfferingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CourseOfferingId",
                table: "Enrollments",
                newName: "CourseSectionId");
        }
    }
}
