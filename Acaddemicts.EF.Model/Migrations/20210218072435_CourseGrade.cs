using Microsoft.EntityFrameworkCore.Migrations;

namespace Acaddemicts.EF.Model.Migrations
{
    public partial class CourseGrade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Budget",
                table: "Departments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "InstructorPersonId",
                table: "Departments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CourseGrade",
                columns: table => new
                {
                    EnrollmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: true),
                    StudentId = table.Column<int>(type: "int", nullable: true),
                    Grade = table.Column<decimal>(type: "decimal(18,6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseGrade", x => x.EnrollmentId);
                    table.ForeignKey(
                        name: "FK_CourseGrade_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseGrade_Persons_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_InstructorPersonId",
                table: "Departments",
                column: "InstructorPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseGrade_CourseId",
                table: "CourseGrade",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseGrade_StudentId",
                table: "CourseGrade",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Persons_InstructorPersonId",
                table: "Departments",
                column: "InstructorPersonId",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Persons_InstructorPersonId",
                table: "Departments");

            migrationBuilder.DropTable(
                name: "CourseGrade");

            migrationBuilder.DropIndex(
                name: "IX_Departments_InstructorPersonId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "Budget",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "InstructorPersonId",
                table: "Departments");
        }
    }
}
