using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace hNext.DbAccessMSSQLCore.Migrations
{
    public partial class CaseHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocumentRegistries",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Number = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(type: "date", nullable: true),
                    AuthorId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentRegistries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentRegistries_People_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CaseHistories",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    PatientId = table.Column<long>(nullable: false),
                    HospitalId = table.Column<int>(nullable: false),
                    DepartmentId = table.Column<int>(nullable: true),
                    Admitted = table.Column<DateTime>(type: "date", nullable: false),
                    Discharged = table.Column<DateTime>(type: "date", nullable: true),
                    Result = table.Column<byte>(nullable: true),
                    Severity = table.Column<byte>(nullable: true),
                    Urgency = table.Column<byte>(nullable: true),
                    ReferredById = table.Column<int>(nullable: true),
                    Insured = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CaseHistories_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CaseHistories_Hospitals_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "Hospitals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaseHistories_DocumentRegistries_Id",
                        column: x => x.Id,
                        principalTable: "DocumentRegistries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaseHistories_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CaseHistories_Hospitals_ReferredById",
                        column: x => x.ReferredById,
                        principalTable: "Hospitals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CaseHistories_DepartmentId",
                table: "CaseHistories",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CaseHistories_HospitalId",
                table: "CaseHistories",
                column: "HospitalId");

            migrationBuilder.CreateIndex(
                name: "IX_CaseHistories_PatientId",
                table: "CaseHistories",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_CaseHistories_ReferredById",
                table: "CaseHistories",
                column: "ReferredById");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentRegistries_AuthorId",
                table: "DocumentRegistries",
                column: "AuthorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CaseHistories");

            migrationBuilder.DropTable(
                name: "DocumentRegistries");
        }
    }
}
