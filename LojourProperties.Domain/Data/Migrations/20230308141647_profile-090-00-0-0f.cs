using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojourProperties.Domain.Data.Migrations
{
    public partial class profile0900000f : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "PropertyDocuments",
                newName: "Note");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "PropertyDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "DocumentCategoryId",
                table: "PropertyDocuments",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DocumentCategories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentCategories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertyDocuments_DocumentCategoryId",
                table: "PropertyDocuments",
                column: "DocumentCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyDocuments_DocumentCategories_DocumentCategoryId",
                table: "PropertyDocuments",
                column: "DocumentCategoryId",
                principalTable: "DocumentCategories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyDocuments_DocumentCategories_DocumentCategoryId",
                table: "PropertyDocuments");

            migrationBuilder.DropTable(
                name: "DocumentCategories");

            migrationBuilder.DropIndex(
                name: "IX_PropertyDocuments_DocumentCategoryId",
                table: "PropertyDocuments");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "PropertyDocuments");

            migrationBuilder.DropColumn(
                name: "DocumentCategoryId",
                table: "PropertyDocuments");

            migrationBuilder.RenameColumn(
                name: "Note",
                table: "PropertyDocuments",
                newName: "Name");
        }
    }
}
