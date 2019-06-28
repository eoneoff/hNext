using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace hNext.DbAccessMSSQLCore.Migrations
{
    public partial class AddDateToDiploma : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "DoctorPositions",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<DateTime>(
                name: "WhenIssued",
                table: "Diplomas",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WhenIssued",
                table: "Diplomas");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "DoctorPositions",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
