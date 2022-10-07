using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Exam.EF.Migrations
{
    public partial class Delete_LevelId_from_TbDepartment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LevelId",
                table: "TbDepartments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LevelId",
                table: "TbDepartments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
