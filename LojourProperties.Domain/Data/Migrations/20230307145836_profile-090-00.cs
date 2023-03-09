using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojourProperties.Domain.Data.Migrations
{
    public partial class profile09000 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "PropertyImages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "PropertyImages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "PropertyImages");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "PropertyImages");
        }
    }
}
