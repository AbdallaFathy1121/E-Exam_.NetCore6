using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Exam.EF.Migrations
{
    public partial class Update_TbQuestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CorrectAnswer",
                table: "TbQuestions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorrectAnswer",
                table: "TbQuestions");
        }
    }
}
