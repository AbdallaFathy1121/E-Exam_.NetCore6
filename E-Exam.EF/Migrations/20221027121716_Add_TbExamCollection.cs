using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Exam.EF.Migrations
{
    public partial class Add_TbExamCollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TbExamCollectionId",
                table: "TbModelTypes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TbExamCollectionId",
                table: "TbChapters",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TbExamCollections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberOfQuestions = table.Column<int>(type: "int", nullable: false),
                    ExamId = table.Column<int>(type: "int", nullable: false),
                    ChapterId = table.Column<int>(type: "int", nullable: false),
                    ModelTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbExamCollections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TbExamTbExamCollection",
                columns: table => new
                {
                    ExamCollectionsId = table.Column<int>(type: "int", nullable: false),
                    ExamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbExamTbExamCollection", x => new { x.ExamCollectionsId, x.ExamId });
                    table.ForeignKey(
                        name: "FK_TbExamTbExamCollection_TbExamCollections_ExamCollectionsId",
                        column: x => x.ExamCollectionsId,
                        principalTable: "TbExamCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TbExamTbExamCollection_TbExams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "TbExams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TbModelTypes_TbExamCollectionId",
                table: "TbModelTypes",
                column: "TbExamCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_TbChapters_TbExamCollectionId",
                table: "TbChapters",
                column: "TbExamCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_TbExamTbExamCollection_ExamId",
                table: "TbExamTbExamCollection",
                column: "ExamId");

            migrationBuilder.AddForeignKey(
                name: "FK_TbChapters_TbExamCollections_TbExamCollectionId",
                table: "TbChapters",
                column: "TbExamCollectionId",
                principalTable: "TbExamCollections",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TbModelTypes_TbExamCollections_TbExamCollectionId",
                table: "TbModelTypes",
                column: "TbExamCollectionId",
                principalTable: "TbExamCollections",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbChapters_TbExamCollections_TbExamCollectionId",
                table: "TbChapters");

            migrationBuilder.DropForeignKey(
                name: "FK_TbModelTypes_TbExamCollections_TbExamCollectionId",
                table: "TbModelTypes");

            migrationBuilder.DropTable(
                name: "TbExamTbExamCollection");

            migrationBuilder.DropTable(
                name: "TbExamCollections");

            migrationBuilder.DropIndex(
                name: "IX_TbModelTypes_TbExamCollectionId",
                table: "TbModelTypes");

            migrationBuilder.DropIndex(
                name: "IX_TbChapters_TbExamCollectionId",
                table: "TbChapters");

            migrationBuilder.DropColumn(
                name: "TbExamCollectionId",
                table: "TbModelTypes");

            migrationBuilder.DropColumn(
                name: "TbExamCollectionId",
                table: "TbChapters");
        }
    }
}
