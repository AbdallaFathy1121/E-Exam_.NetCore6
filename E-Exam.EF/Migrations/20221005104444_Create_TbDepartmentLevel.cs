using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Exam.EF.Migrations
{
    public partial class Create_TbDepartmentLevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbDepartments_TbLevels_LevelId",
                table: "TbDepartments");

            migrationBuilder.DropIndex(
                name: "IX_TbDepartments_LevelId",
                table: "TbDepartments");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "TbLevels");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "TbLevels");

            migrationBuilder.CreateTable(
                name: "TbDepartmentLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LevelId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbDepartmentLevels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbDepartmentLevels_TbDepartments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "TbDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TbDepartmentLevels_TbLevels_LevelId",
                        column: x => x.LevelId,
                        principalTable: "TbLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TbDepartmentLevels_DepartmentId",
                table: "TbDepartmentLevels",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TbDepartmentLevels_LevelId",
                table: "TbDepartmentLevels",
                column: "LevelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TbDepartmentLevels");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "TbLevels",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "TbLevels",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TbDepartments_LevelId",
                table: "TbDepartments",
                column: "LevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_TbDepartments_TbLevels_LevelId",
                table: "TbDepartments",
                column: "LevelId",
                principalTable: "TbLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
