using Microsoft.EntityFrameworkCore.Migrations;

namespace hNext.DbAccessMSSQLCore.Migrations
{
    public partial class ChangeRecordFieldRecordDeleteBehaviour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecordFields_Records_RecordId",
                table: "RecordFields");

            migrationBuilder.AddForeignKey(
                name: "FK_RecordFields_Records_RecordId",
                table: "RecordFields",
                column: "RecordId",
                principalTable: "Records",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecordFields_Records_RecordId",
                table: "RecordFields");

            migrationBuilder.AddForeignKey(
                name: "FK_RecordFields_Records_RecordId",
                table: "RecordFields",
                column: "RecordId",
                principalTable: "Records",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
