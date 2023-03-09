using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojourProperties.Domain.Data.Migrations
{
    public partial class profile09 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AgentId",
                table: "Properties",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "AgentId1",
                table: "Properties",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Properties_AgentId1",
                table: "Properties",
                column: "AgentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_AspNetUsers_AgentId1",
                table: "Properties",
                column: "AgentId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_AspNetUsers_AgentId1",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Properties_AgentId1",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "AgentId",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "AgentId1",
                table: "Properties");
        }
    }
}
