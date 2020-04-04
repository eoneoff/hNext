using Microsoft.EntityFrameworkCore.Migrations;

namespace hNext.DbAccessMSSQLCore.Migrations
{
    public partial class DrugManufacturerColumntCorrectName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Manufatcturer",
                table: "Drugs");

            migrationBuilder.AddColumn<string>(
                name: "Manufacturer",
                table: "Drugs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Manufacturer",
                table: "Drugs");

            migrationBuilder.AddColumn<string>(
                name: "Manufatcturer",
                table: "Drugs",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
