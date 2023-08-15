using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojourProperties.Domain.Data.Migrations
{
    public partial class daxt00 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperatingRegions_CategoryLocations_CategoryId",
                table: "OperatingRegions");

            migrationBuilder.AlterColumn<long>(
                name: "CategoryId",
                table: "OperatingRegions",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_OperatingRegions_CategoryLocations_CategoryId",
                table: "OperatingRegions",
                column: "CategoryId",
                principalTable: "CategoryLocations",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperatingRegions_CategoryLocations_CategoryId",
                table: "OperatingRegions");

            migrationBuilder.AlterColumn<long>(
                name: "CategoryId",
                table: "OperatingRegions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OperatingRegions_CategoryLocations_CategoryId",
                table: "OperatingRegions",
                column: "CategoryId",
                principalTable: "CategoryLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
