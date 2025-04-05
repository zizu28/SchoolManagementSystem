using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Students.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedStudentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AcademicRecords_StudentId",
                table: "AcademicRecords");

            migrationBuilder.DropColumn(
                name: "AcademicRecordId",
                table: "Students");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicRecords_StudentId",
                table: "AcademicRecords",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AcademicRecords_StudentId",
                table: "AcademicRecords");

            migrationBuilder.AddColumn<Guid>(
                name: "AcademicRecordId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AcademicRecords_StudentId",
                table: "AcademicRecords",
                column: "StudentId",
                unique: true);
        }
    }
}
