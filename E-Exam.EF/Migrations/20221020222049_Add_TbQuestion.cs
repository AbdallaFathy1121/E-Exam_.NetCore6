using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Exam.EF.Migrations
{
    public partial class Add_TbQuestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "TbModels",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateTable(
                name: "TbQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModelId = table.Column<int>(type: "int", nullable: false),
                    QuestionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Q1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Q2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Q3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q4 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbQuestions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TbModelTbQuestion",
                columns: table => new
                {
                    ModelId = table.Column<int>(type: "int", nullable: false),
                    QuestionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbModelTbQuestion", x => new { x.ModelId, x.QuestionsId });
                    table.ForeignKey(
                        name: "FK_TbModelTbQuestion_TbModels_ModelId",
                        column: x => x.ModelId,
                        principalTable: "TbModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TbModelTbQuestion_TbQuestions_QuestionsId",
                        column: x => x.QuestionsId,
                        principalTable: "TbQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TbModelTbQuestion_QuestionsId",
                table: "TbModelTbQuestion",
                column: "QuestionsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TbModelTbQuestion");

            migrationBuilder.DropTable(
                name: "TbQuestions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "TbModels",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
