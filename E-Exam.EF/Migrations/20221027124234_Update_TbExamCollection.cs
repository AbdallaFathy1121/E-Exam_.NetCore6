using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Exam.EF.Migrations
{
    public partial class Update_TbExamCollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TbExamCollectionId",
                table: "TbSubjects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "TbExamCollections",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TbSubjects_TbExamCollectionId",
                table: "TbSubjects",
                column: "TbExamCollectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_TbSubjects_TbExamCollections_TbExamCollectionId",
                table: "TbSubjects",
                column: "TbExamCollectionId",
                principalTable: "TbExamCollections",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbSubjects_TbExamCollections_TbExamCollectionId",
                table: "TbSubjects");

            migrationBuilder.DropIndex(
                name: "IX_TbSubjects_TbExamCollectionId",
                table: "TbSubjects");

            migrationBuilder.DropColumn(
                name: "TbExamCollectionId",
                table: "TbSubjects");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "TbExamCollections");
        }
    }
}
