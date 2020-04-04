using Microsoft.EntityFrameworkCore.Migrations;

namespace hNext.DbAccessMSSQLCore.Migrations
{
    public partial class ChangedDrugModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DrugComponents");

            migrationBuilder.DropTable(
                name: "DrugDosages");

            migrationBuilder.DropTable(
                name: "DrugSubstances");

            migrationBuilder.DropColumn(
                name: "Manufacturer",
                table: "Drugs");

            migrationBuilder.DropColumn(
                name: "eHealthId",
                table: "Drugs");

            migrationBuilder.AddColumn<string>(
                name: "ATC",
                table: "Drugs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Compound",
                table: "Drugs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Form",
                table: "Drugs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Group",
                table: "Drugs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InternationalName",
                table: "Drugs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Manufatcturer",
                table: "Drugs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drugs_InternationalName",
                table: "Drugs",
                column: "InternationalName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Drugs_InternationalName",
                table: "Drugs");

            migrationBuilder.DropColumn(
                name: "ATC",
                table: "Drugs");

            migrationBuilder.DropColumn(
                name: "Compound",
                table: "Drugs");

            migrationBuilder.DropColumn(
                name: "Form",
                table: "Drugs");

            migrationBuilder.DropColumn(
                name: "Group",
                table: "Drugs");

            migrationBuilder.DropColumn(
                name: "InternationalName",
                table: "Drugs");

            migrationBuilder.DropColumn(
                name: "Manufatcturer",
                table: "Drugs");

            migrationBuilder.AddColumn<string>(
                name: "Manufacturer",
                table: "Drugs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "eHealthId",
                table: "Drugs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DrugSubstances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ATC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternationalName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    eHealthId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugSubstances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DrugDosages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dosage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SubstanceId = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugDosages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DrugDosages_DrugSubstances_SubstanceId",
                        column: x => x.SubstanceId,
                        principalTable: "DrugSubstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DrugComponents",
                columns: table => new
                {
                    DrugId = table.Column<int>(type: "int", nullable: false),
                    DrugDosageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugComponents", x => new { x.DrugId, x.DrugDosageId });
                    table.ForeignKey(
                        name: "FK_DrugComponents_DrugDosages_DrugDosageId",
                        column: x => x.DrugDosageId,
                        principalTable: "DrugDosages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DrugComponents_Drugs_DrugId",
                        column: x => x.DrugId,
                        principalTable: "Drugs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DrugComponents_DrugDosageId",
                table: "DrugComponents",
                column: "DrugDosageId");

            migrationBuilder.CreateIndex(
                name: "IX_DrugDosages_SubstanceId",
                table: "DrugDosages",
                column: "SubstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_DrugSubstances_InternationalName",
                table: "DrugSubstances",
                column: "InternationalName");
        }
    }
}
