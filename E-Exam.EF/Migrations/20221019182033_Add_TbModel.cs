using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Exam.EF.Migrations
{
    public partial class Add_TbModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TbModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    ModelTypeId = table.Column<int>(type: "int", nullable: false),
                    ChapterId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbModels_TbChapters_ChapterId",
                        column: x => x.ChapterId,
                        principalTable: "TbChapters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TbModels_TbModelTypes_ModelTypeId",
                        column: x => x.ModelTypeId,
                        principalTable: "TbModelTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TbModels_TbSubjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "TbSubjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TbModels_ChapterId",
                table: "TbModels",
                column: "ChapterId");

            migrationBuilder.CreateIndex(
                name: "IX_TbModels_ModelTypeId",
                table: "TbModels",
                column: "ModelTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TbModels_SubjectId",
                table: "TbModels",
                column: "SubjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TbModels");
        }
    }
}
