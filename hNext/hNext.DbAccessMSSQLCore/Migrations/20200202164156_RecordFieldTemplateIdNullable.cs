using Microsoft.EntityFrameworkCore.Migrations;

namespace hNext.DbAccessMSSQLCore.Migrations
{
    public partial class RecordFieldTemplateIdNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecordFields_RecordFieldTemplates_RecordFieldTemplateId",
                table: "RecordFields");

            migrationBuilder.AlterColumn<int>(
                name: "RecordFieldTemplateId",
                table: "RecordFields",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_RecordFields_RecordFieldTemplates_RecordFieldTemplateId",
                table: "RecordFields",
                column: "RecordFieldTemplateId",
                principalTable: "RecordFieldTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecordFields_RecordFieldTemplates_RecordFieldTemplateId",
                table: "RecordFields");

            migrationBuilder.AlterColumn<int>(
                name: "RecordFieldTemplateId",
                table: "RecordFields",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RecordFields_RecordFieldTemplates_RecordFieldTemplateId",
                table: "RecordFields",
                column: "RecordFieldTemplateId",
                principalTable: "RecordFieldTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
