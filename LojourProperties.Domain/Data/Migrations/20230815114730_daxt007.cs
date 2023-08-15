using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojourProperties.Domain.Data.Migrations
{
    public partial class daxt007 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "DisplayTitle",
                table: "OperatingRegions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OperatingRegionId",
                table: "CategoryLocations",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CategoryLocations_OperatingRegionId",
                table: "CategoryLocations",
                column: "OperatingRegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryLocations_OperatingRegions_OperatingRegionId",
                table: "CategoryLocations",
                column: "OperatingRegionId",
                principalTable: "OperatingRegions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryLocations_OperatingRegions_OperatingRegionId",
                table: "CategoryLocations");

            migrationBuilder.DropIndex(
                name: "IX_CategoryLocations_OperatingRegionId",
                table: "CategoryLocations");

            migrationBuilder.DropColumn(
                name: "DisplayTitle",
                table: "OperatingRegions");

            migrationBuilder.DropColumn(
                name: "OperatingRegionId",
                table: "CategoryLocations");

            migrationBuilder.AddColumn<long>(
                name: "CategoryId",
                table: "OperatingRegions",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OperatingRegions_CategoryId",
                table: "OperatingRegions",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_OperatingRegions_CategoryLocations_CategoryId",
                table: "OperatingRegions",
                column: "CategoryId",
                principalTable: "CategoryLocations",
                principalColumn: "Id");
        }
    }
}
