using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojourProperties.Domain.Data.Migrations
{
    public partial class profile090 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_AspNetUsers_AgentId1",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Properties_AgentId1",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "AgentId1",
                table: "Properties");

            migrationBuilder.AlterColumn<string>(
                name: "AgentId",
                table: "Properties",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_AgentId",
                table: "Properties",
                column: "AgentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_AspNetUsers_AgentId",
                table: "Properties",
                column: "AgentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_AspNetUsers_AgentId",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Properties_AgentId",
                table: "Properties");

            migrationBuilder.AlterColumn<long>(
                name: "AgentId",
                table: "Properties",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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
    }
}
