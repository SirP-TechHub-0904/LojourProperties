using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojourProperties.Domain.Data.Migrations
{
    public partial class daxt0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "OperatingRegions");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "OperatingRegions");

            migrationBuilder.AddColumn<long>(
                name: "CategoryId",
                table: "OperatingRegions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_OperatingRegions_CategoryId",
                table: "OperatingRegions",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_OperatingRegions_CategoryLocations_CategoryId",
                table: "OperatingRegions",
                column: "CategoryId",
                principalTable: "CategoryLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperatingRegions_CategoryLocations_CategoryId",
                table: "OperatingRegions");

            migrationBuilder.DropIndex(
                name: "IX_OperatingRegions_CategoryId",
                table: "OperatingRegions");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "OperatingRegions");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "OperatingRegions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "OperatingRegions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
