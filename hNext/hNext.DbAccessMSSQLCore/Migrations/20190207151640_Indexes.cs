using Microsoft.EntityFrameworkCore.Migrations;

namespace hNext.DbAccessMSSQLCore.Migrations
{
    public partial class Indexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Streets",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "FamilyName",
                table: "People",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Districts",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Cities",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Streets_Name",
                table: "Streets",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Phones_Number",
                table: "Phones",
                column: "Number");

            migrationBuilder.CreateIndex(
                name: "IX_People_DateOfBirth",
                table: "People",
                column: "DateOfBirth");

            migrationBuilder.CreateIndex(
                name: "IX_People_FamilyName",
                table: "People",
                column: "FamilyName");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_Name",
                table: "Districts",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Name",
                table: "Cities",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Streets_Name",
                table: "Streets");

            migrationBuilder.DropIndex(
                name: "IX_Phones_Number",
                table: "Phones");

            migrationBuilder.DropIndex(
                name: "IX_People_DateOfBirth",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_FamilyName",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_Districts_Name",
                table: "Districts");

            migrationBuilder.DropIndex(
                name: "IX_Cities_Name",
                table: "Cities");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Streets",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "FamilyName",
                table: "People",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Districts",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Cities",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
