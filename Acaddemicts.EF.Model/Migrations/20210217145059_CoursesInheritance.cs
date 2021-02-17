using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Acaddemicts.EF.Model.Migrations
{
    public partial class CoursesInheritance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Days",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsOnLine",
                table: "Courses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "Courses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Days",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "IsOnLine",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Courses");
        }
    }
}
