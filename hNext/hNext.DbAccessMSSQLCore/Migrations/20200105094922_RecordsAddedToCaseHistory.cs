using Microsoft.EntityFrameworkCore.Migrations;

namespace hNext.DbAccessMSSQLCore.Migrations
{
    public partial class RecordsAddedToCaseHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DiagnosysId",
                table: "Records",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderNo",
                table: "RecordFields",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "OrderNo",
                table: "RecordFields");
        }
    }
}
