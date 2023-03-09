using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojourProperties.Domain.Data.Migrations
{
    public partial class datadoc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PropertyDocuments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Permitted = table.Column<bool>(type: "bit", nullable: false),
                    PropertyId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyDocuments_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertyDocuments_PropertyId",
                table: "PropertyDocuments",
                column: "PropertyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyDocuments");
        }
    }
}
