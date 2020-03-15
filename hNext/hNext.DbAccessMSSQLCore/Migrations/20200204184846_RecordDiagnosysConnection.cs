using Microsoft.EntityFrameworkCore.Migrations;

namespace hNext.DbAccessMSSQLCore.Migrations
{
    public partial class RecordDiagnosysConnection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Records_Diagnoses_DiagnosysId",
                table: "Records");

            migrationBuilder.DropIndex(
                name: "IX_Records_DiagnosysId",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "DiagnosysId",
                table: "Records");

            migrationBuilder.AddColumn<long>(
                name: "RecordsId",
                table: "PatientDiagnoses",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RecordDiagnoses",
                columns: table => new
                {
                    RecordId = table.Column<long>(nullable: false),
                    DiagnosysId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordDiagnoses", x => new { x.RecordId, x.DiagnosysId });
                    table.ForeignKey(
                        name: "FK_RecordDiagnoses_Diagnoses_DiagnosysId",
                        column: x => x.DiagnosysId,
                        principalTable: "Diagnoses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecordDiagnoses_Records_RecordId",
                        column: x => x.RecordId,
                        principalTable: "Records",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientDiagnoses_RecordsId",
                table: "PatientDiagnoses",
                column: "RecordsId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordDiagnoses_DiagnosysId",
                table: "RecordDiagnoses",
                column: "DiagnosysId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientDiagnoses_Records_RecordsId",
                table: "PatientDiagnoses",
                column: "RecordsId",
                principalTable: "Records",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientDiagnoses_Records_RecordsId",
                table: "PatientDiagnoses");

            migrationBuilder.DropTable(
                name: "RecordDiagnoses");

            migrationBuilder.DropIndex(
                name: "IX_PatientDiagnoses_RecordsId",
                table: "PatientDiagnoses");

            migrationBuilder.DropColumn(
                name: "RecordsId",
                table: "PatientDiagnoses");

            migrationBuilder.AddColumn<long>(
                name: "DiagnosysId",
                table: "Records",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Records_DiagnosysId",
                table: "Records",
                column: "DiagnosysId");

            migrationBuilder.AddForeignKey(
                name: "FK_Records_Diagnoses_DiagnosysId",
                table: "Records",
                column: "DiagnosysId",
                principalTable: "Diagnoses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
