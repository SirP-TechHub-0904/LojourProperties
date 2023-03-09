using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojourProperties.Domain.Data.Migrations
{
    public partial class datastateslga : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "OperatingRegionId",
                table: "Properties",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocalGoverments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LGAName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalGoverments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocalGoverments_States_StatesId",
                        column: x => x.StatesId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Properties_OperatingRegionId",
                table: "Properties",
                column: "OperatingRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalGoverments_StatesId",
                table: "LocalGoverments",
                column: "StatesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_OperatingRegions_OperatingRegionId",
                table: "Properties",
                column: "OperatingRegionId",
                principalTable: "OperatingRegions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_OperatingRegions_OperatingRegionId",
                table: "Properties");

            migrationBuilder.DropTable(
                name: "LocalGoverments");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropIndex(
                name: "IX_Properties_OperatingRegionId",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "OperatingRegionId",
                table: "Properties");
        }
    }
}
