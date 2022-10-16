using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Exam.EF.Migrations
{
    public partial class Add_TbSubject_TbSubjectDepartment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TbSubjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LevelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbSubjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbSubjects_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TbSubjects_TbLevels_LevelId",
                        column: x => x.LevelId,
                        principalTable: "TbLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TbSubjectDepartments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbSubjectDepartments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbSubjectDepartments_TbDepartments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "TbDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TbSubjectDepartments_TbSubjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "TbSubjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TbSubjectDepartments_DepartmentId",
                table: "TbSubjectDepartments",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TbSubjectDepartments_SubjectId",
                table: "TbSubjectDepartments",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TbSubjects_LevelId",
                table: "TbSubjects",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_TbSubjects_UserId",
                table: "TbSubjects",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TbSubjectDepartments");

            migrationBuilder.DropTable(
                name: "TbSubjects");
        }
    }
}
