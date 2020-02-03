using Microsoft.EntityFrameworkCore.Migrations;

namespace hNext.DbAccessMSSQLCore.Migrations
{
    public partial class MakeRecordTemplateNotRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Records_RecordTemplates_RecordTemplateId",
                table: "Records");

            migrationBuilder.AlterColumn<int>(
                name: "RecordTemplateId",
                table: "Records",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Records_RecordTemplates_RecordTemplateId",
                table: "Records",
                column: "RecordTemplateId",
                principalTable: "RecordTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Records_RecordTemplates_RecordTemplateId",
                table: "Records");

            migrationBuilder.AlterColumn<int>(
                name: "RecordTemplateId",
                table: "Records",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Records_RecordTemplates_RecordTemplateId",
                table: "Records",
                column: "RecordTemplateId",
                principalTable: "RecordTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
