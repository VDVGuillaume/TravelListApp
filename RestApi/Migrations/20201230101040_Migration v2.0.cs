using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelList.Api.Migrations
{
    public partial class Migrationv20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    TravelRouteID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TravelListItemID = table.Column<int>(nullable: false),
                    StartTravelPointOfInterestID = table.Column<int>(nullable: false),
                    EndTravelPointOfInterestID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.TravelRouteID);
                    table.ForeignKey(
                        name: "FK_Routes_TravelLists_TravelListItemID",
                        column: x => x.TravelListItemID,
                        principalTable: "TravelLists",
                        principalColumn: "TravelListItemID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Routes_TravelListItemID",
                table: "Routes",
                column: "TravelListItemID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Routes");
        }
    }
}
