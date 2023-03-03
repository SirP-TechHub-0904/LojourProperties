using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojourProperties.Domain.Data.Migrations
{
    public partial class profile45 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_OperatingRegions_OperatingRegionId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<long>(
                name: "OperatingRegionId",
                table: "AspNetUsers",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_OperatingRegions_OperatingRegionId",
                table: "AspNetUsers",
                column: "OperatingRegionId",
                principalTable: "OperatingRegions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_OperatingRegions_OperatingRegionId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<long>(
                name: "OperatingRegionId",
                table: "AspNetUsers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_OperatingRegions_OperatingRegionId",
                table: "AspNetUsers",
                column: "OperatingRegionId",
                principalTable: "OperatingRegions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
