﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojourProperties.Domain.Data.Migrations
{
    public partial class intfeature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "FeaturesCategories",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "FeaturesCategories");
        }
    }
}
