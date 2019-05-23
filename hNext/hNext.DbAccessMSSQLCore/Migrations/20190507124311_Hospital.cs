using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace hNext.DbAccessMSSQLCore.Migrations
{
    public partial class Hospital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HospitalTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    eHealtId = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HospitalTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PropertyTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    eHealthId = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hospitals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    ShortName = table.Column<string>(nullable: true),
                    EDRPOU = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    Login = table.Column<string>(nullable: true),
                    AddressId = table.Column<long>(nullable: false),
                    HospitalTypeId = table.Column<int>(nullable: false),
                    PropertyTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hospitals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hospitals_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Hospitals_HospitalTypes_HospitalTypeId",
                        column: x => x.HospitalTypeId,
                        principalTable: "HospitalTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Hospitals_PropertyTypes_PropertyTypeId",
                        column: x => x.PropertyTypeId,
                        principalTable: "PropertyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HospitalEmails",
                columns: table => new
                {
                    HospitalId = table.Column<int>(nullable: false),
                    EmailId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HospitalEmails", x => new { x.HospitalId, x.EmailId });
                    table.ForeignKey(
                        name: "FK_HospitalEmails_Emails_EmailId",
                        column: x => x.EmailId,
                        principalTable: "Emails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HospitalEmails_Hospitals_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "Hospitals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HospitalPhones",
                columns: table => new
                {
                    HospitalId = table.Column<int>(nullable: false),
                    PhoneId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HospitalPhones", x => new { x.HospitalId, x.PhoneId });
                    table.ForeignKey(
                        name: "FK_HospitalPhones_Hospitals_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "Hospitals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HospitalPhones_Phones_PhoneId",
                        column: x => x.PhoneId,
                        principalTable: "Phones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HospitalEmails_EmailId",
                table: "HospitalEmails",
                column: "EmailId");

            migrationBuilder.CreateIndex(
                name: "IX_HospitalPhones_PhoneId",
                table: "HospitalPhones",
                column: "PhoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Hospitals_AddressId",
                table: "Hospitals",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Hospitals_HospitalTypeId",
                table: "Hospitals",
                column: "HospitalTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Hospitals_Name",
                table: "Hospitals",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Hospitals_PropertyTypeId",
                table: "Hospitals",
                column: "PropertyTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HospitalEmails");

            migrationBuilder.DropTable(
                name: "HospitalPhones");

            migrationBuilder.DropTable(
                name: "Hospitals");

            migrationBuilder.DropTable(
                name: "HospitalTypes");

            migrationBuilder.DropTable(
                name: "PropertyTypes");
        }
    }
}
