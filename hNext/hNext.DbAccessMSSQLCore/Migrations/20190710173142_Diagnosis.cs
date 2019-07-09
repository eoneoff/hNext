using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace hNext.DbAccessMSSQLCore.Migrations
{
    public partial class Diagnosis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ICD",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Letter = table.Column<string>(maxLength: 1, nullable: false),
                    PrimaryNumber = table.Column<int>(nullable: false),
                    SecondaryNumber = table.Column<int>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    SubCategory = table.Column<string>(nullable: true),
                    PrimaryName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ICD", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Diagnoses",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    ICDId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnoses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Diagnoses_ICD_ICDId",
                        column: x => x.ICDId,
                        principalTable: "ICD",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CaseHistoryDiagnoses",
                columns: table => new
                {
                    CaseHistoryId = table.Column<long>(nullable: false),
                    DiagnosysId = table.Column<long>(nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: true),
                    Type = table.Column<byte>(nullable: true),
                    WhenSet = table.Column<byte>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseHistoryDiagnoses", x => new { x.CaseHistoryId, x.DiagnosysId });
                    table.ForeignKey(
                        name: "FK_CaseHistoryDiagnoses_CaseHistories_CaseHistoryId",
                        column: x => x.CaseHistoryId,
                        principalTable: "CaseHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaseHistoryDiagnoses_Diagnoses_DiagnosysId",
                        column: x => x.DiagnosysId,
                        principalTable: "Diagnoses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientDiagnoses",
                columns: table => new
                {
                    PatientId = table.Column<long>(nullable: false),
                    DiagnosysId = table.Column<long>(nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientDiagnoses", x => new { x.PatientId, x.DiagnosysId });
                    table.ForeignKey(
                        name: "FK_PatientDiagnoses_Diagnoses_DiagnosysId",
                        column: x => x.DiagnosysId,
                        principalTable: "Diagnoses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientDiagnoses_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CaseHistoryDiagnoses_DiagnosysId",
                table: "CaseHistoryDiagnoses",
                column: "DiagnosysId");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnoses_ICDId",
                table: "Diagnoses",
                column: "ICDId");

            migrationBuilder.CreateIndex(
                name: "IX_ICD_Letter",
                table: "ICD",
                column: "Letter");

            migrationBuilder.CreateIndex(
                name: "IX_PatientDiagnoses_DiagnosysId",
                table: "PatientDiagnoses",
                column: "DiagnosysId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CaseHistoryDiagnoses");

            migrationBuilder.DropTable(
                name: "PatientDiagnoses");

            migrationBuilder.DropTable(
                name: "Diagnoses");

            migrationBuilder.DropTable(
                name: "ICD");
        }
    }
}
