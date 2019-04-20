using Microsoft.EntityFrameworkCore.Migrations;

namespace hNext.DbAccessMSSQLCore.Migrations
{
    public partial class Documents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Document_DocumentType_DocumentTypeId",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_Document_People_PersonId",
                table: "Document");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentType",
                table: "DocumentType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Document",
                table: "Document");

            migrationBuilder.RenameTable(
                name: "DocumentType",
                newName: "DocumentTypes");

            migrationBuilder.RenameTable(
                name: "Document",
                newName: "Documents");

            migrationBuilder.RenameIndex(
                name: "IX_Document_PersonId",
                table: "Documents",
                newName: "IX_Documents_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Document_DocumentTypeId",
                table: "Documents",
                newName: "IX_Documents_DocumentTypeId");

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "Documents",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentTypes",
                table: "DocumentTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Documents",
                table: "Documents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_DocumentTypes_DocumentTypeId",
                table: "Documents",
                column: "DocumentTypeId",
                principalTable: "DocumentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_People_PersonId",
                table: "Documents",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_DocumentTypes_DocumentTypeId",
                table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_Documents_People_PersonId",
                table: "Documents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentTypes",
                table: "DocumentTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Documents",
                table: "Documents");

            migrationBuilder.RenameTable(
                name: "DocumentTypes",
                newName: "DocumentType");

            migrationBuilder.RenameTable(
                name: "Documents",
                newName: "Document");

            migrationBuilder.RenameIndex(
                name: "IX_Documents_PersonId",
                table: "Document",
                newName: "IX_Document_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Documents_DocumentTypeId",
                table: "Document",
                newName: "IX_Document_DocumentTypeId");

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "Document",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentType",
                table: "DocumentType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Document",
                table: "Document",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_DocumentType_DocumentTypeId",
                table: "Document",
                column: "DocumentTypeId",
                principalTable: "DocumentType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Document_People_PersonId",
                table: "Document",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
