using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojourProperties.Domain.Data.Migrations
{
    public partial class daxt00779 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_OperatingRegions_OperatingRegionId",
                table: "Properties");

            migrationBuilder.RenameColumn(
                name: "OperatingRegionId",
                table: "Properties",
                newName: "CityLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_OperatingRegionId",
                table: "Properties",
                newName: "IX_Properties_CityLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_CityLocations_CityLocationId",
                table: "Properties",
                column: "CityLocationId",
                principalTable: "CityLocations",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_CityLocations_CityLocationId",
                table: "Properties");

            migrationBuilder.RenameColumn(
                name: "CityLocationId",
                table: "Properties",
                newName: "OperatingRegionId");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_CityLocationId",
                table: "Properties",
                newName: "IX_Properties_OperatingRegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_OperatingRegions_OperatingRegionId",
                table: "Properties",
                column: "OperatingRegionId",
                principalTable: "OperatingRegions",
                principalColumn: "Id");
        }
    }
}
