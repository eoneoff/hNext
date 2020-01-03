using Microsoft.EntityFrameworkCore.Migrations;

namespace hNext.DbAccessMSSQLCore.Migrations
{
    public partial class RemovedDefaultSelectedOptionFromRecordFieldTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultSelectedOption",
                table: "RecordFieldTemplates");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DefaultSelectedOption",
                table: "RecordFieldTemplates",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
