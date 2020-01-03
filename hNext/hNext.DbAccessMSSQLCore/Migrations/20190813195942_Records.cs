using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace hNext.DbAccessMSSQLCore.Migrations
{
    public partial class Records : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecordTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HospitalId = table.Column<int>(nullable: true),
                    DepartmentId = table.Column<int>(nullable: true),
                    SpecialtyId = table.Column<int>(nullable: true),
                    DoctorId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Header = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecordTemplates_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecordTemplates_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecordTemplates_Hospitals_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "Hospitals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecordTemplates_Specialties_SpecialtyId",
                        column: x => x.SpecialtyId,
                        principalTable: "Specialties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RecordFieldTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Header = table.Column<string>(nullable: false),
                    OrderNo = table.Column<int>(nullable: false),
                    NewLine = table.Column<bool>(nullable: false, defaultValue: false),
                    RecordFieldType = table.Column<byte>(nullable: false),
                    DefaultValue = table.Column<string>(nullable: true),
                    DefaultSelectedOption = table.Column<int>(nullable: false),
                    RecordTemplateId = table.Column<int>(nullable: false),
                    DepartmentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordFieldTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecordFieldTemplates_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecordFieldTemplates_RecordTemplates_RecordTemplateId",
                        column: x => x.RecordTemplateId,
                        principalTable: "RecordTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Records",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    RecordTemplateId = table.Column<int>(nullable: false),
                    PatientId = table.Column<long>(nullable: false),
                    CaseHistoryId = table.Column<long>(nullable: true),
                    SpecialtyId = table.Column<int>(nullable: true),
                    DoctorId = table.Column<long>(nullable: true),
                    Header = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Records", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Records_CaseHistories_CaseHistoryId",
                        column: x => x.CaseHistoryId,
                        principalTable: "CaseHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Records_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Records_DocumentRegistries_Id",
                        column: x => x.Id,
                        principalTable: "DocumentRegistries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Records_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Records_RecordTemplates_RecordTemplateId",
                        column: x => x.RecordTemplateId,
                        principalTable: "RecordTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Records_Specialties_SpecialtyId",
                        column: x => x.SpecialtyId,
                        principalTable: "Specialties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RecordFieldTemplateOptions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RecordFieldTemplateId = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordFieldTemplateOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecordFieldTemplateOptions_RecordFieldTemplates_RecordFieldTemplateId",
                        column: x => x.RecordFieldTemplateId,
                        principalTable: "RecordFieldTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecordFields",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RecordFieldTemplateId = table.Column<int>(nullable: false),
                    RecordId = table.Column<long>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecordFields_RecordFieldTemplates_RecordFieldTemplateId",
                        column: x => x.RecordFieldTemplateId,
                        principalTable: "RecordFieldTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecordFields_Records_RecordId",
                        column: x => x.RecordId,
                        principalTable: "Records",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecordFields_RecordFieldTemplateId",
                table: "RecordFields",
                column: "RecordFieldTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordFields_RecordId",
                table: "RecordFields",
                column: "RecordId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordFieldTemplateOptions_RecordFieldTemplateId",
                table: "RecordFieldTemplateOptions",
                column: "RecordFieldTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordFieldTemplates_DepartmentId",
                table: "RecordFieldTemplates",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordFieldTemplates_RecordTemplateId",
                table: "RecordFieldTemplates",
                column: "RecordTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Records_CaseHistoryId",
                table: "Records",
                column: "CaseHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Records_DoctorId",
                table: "Records",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Records_PatientId",
                table: "Records",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Records_RecordTemplateId",
                table: "Records",
                column: "RecordTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Records_SpecialtyId",
                table: "Records",
                column: "SpecialtyId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordTemplates_DepartmentId",
                table: "RecordTemplates",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordTemplates_DoctorId",
                table: "RecordTemplates",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordTemplates_HospitalId",
                table: "RecordTemplates",
                column: "HospitalId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordTemplates_SpecialtyId",
                table: "RecordTemplates",
                column: "SpecialtyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecordFields");

            migrationBuilder.DropTable(
                name: "RecordFieldTemplateOptions");

            migrationBuilder.DropTable(
                name: "Records");

            migrationBuilder.DropTable(
                name: "RecordFieldTemplates");

            migrationBuilder.DropTable(
                name: "RecordTemplates");
        }
    }
}
