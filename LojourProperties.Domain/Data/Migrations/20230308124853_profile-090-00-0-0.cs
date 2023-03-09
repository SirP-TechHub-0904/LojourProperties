using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojourProperties.Domain.Data.Migrations
{
    public partial class profile0900000 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Default",
                table: "PropertyImages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Default",
                table: "PropertyImages");
        }
    }
}
