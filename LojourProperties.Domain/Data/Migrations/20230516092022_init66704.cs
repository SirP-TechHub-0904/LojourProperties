using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojourProperties.Domain.Data.Migrations
{
    public partial class init66704 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_OperatingRegions_OperatingRegionId",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Properties_OperatingRegionId",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "OperatingRegionId",
                table: "Properties");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "OperatingRegionId",
                table: "Properties",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Properties_OperatingRegionId",
                table: "Properties",
                column: "OperatingRegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_OperatingRegions_OperatingRegionId",
                table: "Properties",
                column: "OperatingRegionId",
                principalTable: "OperatingRegions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
