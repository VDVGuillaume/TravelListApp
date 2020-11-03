using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelList.Api.Migrations
{
    public partial class Migrationv14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TravelPointOfInterest_TravelLists_TravelListItemID",
                table: "TravelPointOfInterest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TravelPointOfInterest",
                table: "TravelPointOfInterest");

            migrationBuilder.RenameTable(
                name: "TravelPointOfInterest",
                newName: "Points");

            migrationBuilder.RenameIndex(
                name: "IX_TravelPointOfInterest_TravelListItemID",
                table: "Points",
                newName: "IX_Points_TravelListItemID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Points",
                table: "Points",
                column: "TravelPointOfInterestID");

            migrationBuilder.AddForeignKey(
                name: "FK_Points_TravelLists_TravelListItemID",
                table: "Points",
                column: "TravelListItemID",
                principalTable: "TravelLists",
                principalColumn: "TravelListItemID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Points_TravelLists_TravelListItemID",
                table: "Points");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Points",
                table: "Points");

            migrationBuilder.RenameTable(
                name: "Points",
                newName: "TravelPointOfInterest");

            migrationBuilder.RenameIndex(
                name: "IX_Points_TravelListItemID",
                table: "TravelPointOfInterest",
                newName: "IX_TravelPointOfInterest_TravelListItemID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TravelPointOfInterest",
                table: "TravelPointOfInterest",
                column: "TravelPointOfInterestID");

            migrationBuilder.AddForeignKey(
                name: "FK_TravelPointOfInterest_TravelLists_TravelListItemID",
                table: "TravelPointOfInterest",
                column: "TravelListItemID",
                principalTable: "TravelLists",
                principalColumn: "TravelListItemID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
