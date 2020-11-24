using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelList.Api.Migrations
{
    public partial class Migrationv16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "TravelListImages",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TravelListImages_TravelListItemID",
                table: "TravelListImages",
                column: "TravelListItemID");

            migrationBuilder.AddForeignKey(
                name: "FK_TravelListImages_TravelLists_TravelListItemID",
                table: "TravelListImages",
                column: "TravelListItemID",
                principalTable: "TravelLists",
                principalColumn: "TravelListItemID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TravelListImages_TravelLists_TravelListItemID",
                table: "TravelListImages");

            migrationBuilder.DropIndex(
                name: "IX_TravelListImages_TravelListItemID",
                table: "TravelListImages");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "TravelListImages");
        }
    }
}
