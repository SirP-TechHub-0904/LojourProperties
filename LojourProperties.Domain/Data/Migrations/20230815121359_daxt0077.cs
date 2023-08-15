using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojourProperties.Domain.Data.Migrations
{
    public partial class daxt0077 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryLocations_OperatingRegions_OperatingRegionId",
                table: "CategoryLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryLocations_States_StateId",
                table: "CategoryLocations");

            migrationBuilder.DropIndex(
                name: "IX_CategoryLocations_StateId",
                table: "CategoryLocations");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "CategoryLocations");

            migrationBuilder.AlterColumn<long>(
                name: "OperatingRegionId",
                table: "CategoryLocations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryLocations_OperatingRegions_OperatingRegionId",
                table: "CategoryLocations",
                column: "OperatingRegionId",
                principalTable: "OperatingRegions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryLocations_OperatingRegions_OperatingRegionId",
                table: "CategoryLocations");

            migrationBuilder.AlterColumn<long>(
                name: "OperatingRegionId",
                table: "CategoryLocations",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "StateId",
                table: "CategoryLocations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_CategoryLocations_StateId",
                table: "CategoryLocations",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryLocations_OperatingRegions_OperatingRegionId",
                table: "CategoryLocations",
                column: "OperatingRegionId",
                principalTable: "OperatingRegions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryLocations_States_StateId",
                table: "CategoryLocations",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
