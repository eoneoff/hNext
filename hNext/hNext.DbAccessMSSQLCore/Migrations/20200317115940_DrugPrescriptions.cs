using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace hNext.DbAccessMSSQLCore.Migrations
{
    public partial class DrugPrescriptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Drugs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Manufacturer = table.Column<string>(nullable: true),
                    eHealthId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drugs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DrugSubstances",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    InternationalName = table.Column<string>(nullable: true),
                    ATC = table.Column<string>(nullable: true),
                    eHealthId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugSubstances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    DoctorId = table.Column<long>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    Duration = table.Column<long>(nullable: false),
                    PatientId = table.Column<long>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    DrugId = table.Column<int>(nullable: true),
                    Dosage = table.Column<string>(nullable: true),
                    Times = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Drugs_DrugId",
                        column: x => x.DrugId,
                        principalTable: "Drugs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DrugDosages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubstanceId = table.Column<int>(nullable: false),
                    Dosage = table.Column<decimal>(nullable: false),
                    Unit = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugDosages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DrugDosages_DrugSubstances_SubstanceId",
                        column: x => x.SubstanceId,
                        principalTable: "DrugSubstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CaseHistoryPrescriptions",
                columns: table => new
                {
                    CaseHistoryId = table.Column<long>(nullable: false),
                    PrescriptionId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseHistoryPrescriptions", x => new { x.CaseHistoryId, x.PrescriptionId });
                    table.ForeignKey(
                        name: "FK_CaseHistoryPrescriptions_CaseHistories_CaseHistoryId",
                        column: x => x.CaseHistoryId,
                        principalTable: "CaseHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaseHistoryPrescriptions_Prescriptions_CaseHistoryId",
                        column: x => x.CaseHistoryId,
                        principalTable: "Prescriptions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RecordPrescriptions",
                columns: table => new
                {
                    RecordId = table.Column<long>(nullable: false),
                    PrescriptionId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordPrescriptions", x => new { x.RecordId, x.PrescriptionId });
                    table.ForeignKey(
                        name: "FK_RecordPrescriptions_Prescriptions_PrescriptionId",
                        column: x => x.PrescriptionId,
                        principalTable: "Prescriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecordPrescriptions_Records_RecordId",
                        column: x => x.RecordId,
                        principalTable: "Records",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DrugComponents",
                columns: table => new
                {
                    DrugId = table.Column<int>(nullable: false),
                    DrugDosageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugComponents", x => new { x.DrugId, x.DrugDosageId });
                    table.ForeignKey(
                        name: "FK_DrugComponents_DrugDosages_DrugDosageId",
                        column: x => x.DrugDosageId,
                        principalTable: "DrugDosages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DrugComponents_Drugs_DrugId",
                        column: x => x.DrugId,
                        principalTable: "Drugs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DrugComponents_DrugDosageId",
                table: "DrugComponents",
                column: "DrugDosageId");

            migrationBuilder.CreateIndex(
                name: "IX_DrugDosages_SubstanceId",
                table: "DrugDosages",
                column: "SubstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Drugs_Name",
                table: "Drugs",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_DrugSubstances_InternationalName",
                table: "DrugSubstances",
                column: "InternationalName");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_DrugId",
                table: "Prescriptions",
                column: "DrugId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_DoctorId",
                table: "Prescriptions",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_PatientId",
                table: "Prescriptions",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordPrescriptions_PrescriptionId",
                table: "RecordPrescriptions",
                column: "PrescriptionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CaseHistoryPrescriptions");

            migrationBuilder.DropTable(
                name: "DrugComponents");

            migrationBuilder.DropTable(
                name: "RecordPrescriptions");

            migrationBuilder.DropTable(
                name: "DrugDosages");

            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropTable(
                name: "DrugSubstances");

            migrationBuilder.DropTable(
                name: "Drugs");
        }
    }
}
