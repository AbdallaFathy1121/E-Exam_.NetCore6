using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Exam.EF.Migrations
{
    public partial class Update_TbLevels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "TbLevels");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "TbLevels");
        }
    }
}
