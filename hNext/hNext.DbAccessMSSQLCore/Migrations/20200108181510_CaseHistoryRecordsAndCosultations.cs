using Microsoft.EntityFrameworkCore.Migrations;

namespace hNext.DbAccessMSSQLCore.Migrations
{
    public partial class CaseHistoryRecordsAndCosultations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Records_CaseHistories_CaseHistoryId",
                table: "Records");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Records",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Records_CaseHistoryId1",
                table: "Records",
                column: "CaseHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Records_CaseHistories_CaseHistoryId",
                table: "Records",
                column: "CaseHistoryId",
                principalTable: "CaseHistories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Records_CaseHistories_CaseHistoryId1",
                table: "Records",
                column: "CaseHistoryId",
                principalTable: "CaseHistories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Records_CaseHistories_CaseHistoryId",
                table: "Records");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_CaseHistories_CaseHistoryId1",
                table: "Records");

            migrationBuilder.DropIndex(
                name: "IX_Records_CaseHistoryId1",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Records");

            migrationBuilder.AddForeignKey(
                name: "FK_Records_CaseHistories_CaseHistoryId",
                table: "Records",
                column: "CaseHistoryId",
                principalTable: "CaseHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
