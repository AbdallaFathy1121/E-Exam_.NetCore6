using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Exam.EF.Migrations
{
    public partial class Add_TbExam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TbExamId",
                table: "TbSubjects",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TbExams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    AccessCode = table.Column<int>(type: "int", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbExams", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TbSubjects_TbExamId",
                table: "TbSubjects",
                column: "TbExamId");

            migrationBuilder.AddForeignKey(
                name: "FK_TbSubjects_TbExams_TbExamId",
                table: "TbSubjects",
                column: "TbExamId",
                principalTable: "TbExams",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbSubjects_TbExams_TbExamId",
                table: "TbSubjects");

            migrationBuilder.DropTable(
                name: "TbExams");

            migrationBuilder.DropIndex(
                name: "IX_TbSubjects_TbExamId",
                table: "TbSubjects");

            migrationBuilder.DropColumn(
                name: "TbExamId",
                table: "TbSubjects");
        }
    }
}
