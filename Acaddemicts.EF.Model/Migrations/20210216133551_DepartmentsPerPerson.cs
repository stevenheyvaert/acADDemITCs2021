using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Acaddemicts.EF.Model.Migrations
{
    public partial class DepartmentsPerPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdministratorId",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsEnrolled = table.Column<bool>(type: "bit", nullable: false),
                    EnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.PersonId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_AdministratorId",
                table: "Departments",
                column: "AdministratorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Persons_AdministratorId",
                table: "Departments",
                column: "AdministratorId",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Persons_AdministratorId",
                table: "Departments");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Departments_AdministratorId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "AdministratorId",
                table: "Departments");
        }
    }
}
