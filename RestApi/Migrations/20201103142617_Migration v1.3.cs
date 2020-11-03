using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelList.Api.Migrations
{
    public partial class Migrationv13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TravelPointOfInterest",
                columns: table => new
                {
                    TravelPointOfInterestID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Latitude = table.Column<decimal>(nullable: false),
                    Longitude = table.Column<decimal>(nullable: false),
                    TravelListItemID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelPointOfInterest", x => x.TravelPointOfInterestID);
                    table.ForeignKey(
                        name: "FK_TravelPointOfInterest_TravelLists_TravelListItemID",
                        column: x => x.TravelListItemID,
                        principalTable: "TravelLists",
                        principalColumn: "TravelListItemID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TravelPointOfInterest_TravelListItemID",
                table: "TravelPointOfInterest",
                column: "TravelListItemID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TravelPointOfInterest");
        }
    }
}
