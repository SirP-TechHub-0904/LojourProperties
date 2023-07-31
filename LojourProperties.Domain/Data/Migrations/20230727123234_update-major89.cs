using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojourProperties.Domain.Data.Migrations
{
    public partial class updatemajor89 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperatingRegions_AspNetUsers_ProfileId",
                table: "OperatingRegions");

            migrationBuilder.DropIndex(
                name: "IX_OperatingRegions_ProfileId",
                table: "OperatingRegions");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "OperatingRegions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfileId",
                table: "OperatingRegions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OperatingRegions_ProfileId",
                table: "OperatingRegions",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_OperatingRegions_AspNetUsers_ProfileId",
                table: "OperatingRegions",
                column: "ProfileId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
