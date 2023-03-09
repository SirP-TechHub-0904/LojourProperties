using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojourProperties.Domain.Data.Migrations
{
    public partial class pagesp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WebPages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    PagePosition = table.Column<int>(type: "int", nullable: false),
                    Publish = table.Column<bool>(type: "bit", nullable: false),
                    TextOne = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TextTwo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TextThree = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TextFour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TextFive = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TextSix = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TextSeven = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TextEight = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TextNine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TextTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TextEleven = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TextTwelve = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TextThirteen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TextFourteen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TextFiften = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TextSixteen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TextSeventeen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TextEighteen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TextNineteen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TextTwenty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageOne = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageTwo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageThree = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageFour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageFive = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageSix = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageSeven = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageEight = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageNine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageEleven = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageTwelve = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageThirteen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageFourteen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageFiften = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageSixteen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageSeventeen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageEighteen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageNineteen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageTwenty = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebPages", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WebPages");
        }
    }
}
