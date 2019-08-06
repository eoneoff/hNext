using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace hNext.DbAccessMSSQLCore.Migrations
{
    public partial class CaseHistoryAdmission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CaseHistories_Departments_DepartmentId",
                table: "CaseHistories");

            migrationBuilder.DropIndex(
                name: "IX_CaseHistories_DepartmentId",
                table: "CaseHistories");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "CaseHistories");

            migrationBuilder.CreateTable(
                name: "CaseHistoryAdmissions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CaseHistoryId = table.Column<long>(nullable: false),
                    DepartmentId = table.Column<int>(nullable: true),
                    Discharged = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseHistoryAdmissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CaseHistoryAdmissions_CaseHistories_CaseHistoryId",
                        column: x => x.CaseHistoryId,
                        principalTable: "CaseHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaseHistoryAdmissions_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CaseHistoryAdmissions_CaseHistoryId",
                table: "CaseHistoryAdmissions",
                column: "CaseHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CaseHistoryAdmissions_DepartmentId",
                table: "CaseHistoryAdmissions",
                column: "DepartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CaseHistoryAdmissions");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "CaseHistories",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CaseHistories_DepartmentId",
                table: "CaseHistories",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_CaseHistories_Departments_DepartmentId",
                table: "CaseHistories",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
