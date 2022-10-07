using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Exam.EF.Migrations
{
    public partial class Create_TbDepartment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TbDepartments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LevelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbDepartments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbDepartments_TbLevels_LevelId",
                        column: x => x.LevelId,
                        principalTable: "TbLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TbDepartments_LevelId",
                table: "TbDepartments",
                column: "LevelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TbDepartments");
        }
    }
}
