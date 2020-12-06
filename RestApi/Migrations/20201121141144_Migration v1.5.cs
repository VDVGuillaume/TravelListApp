using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelList.Api.Migrations
{
    public partial class Migrationv15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TravelListImages",
                columns: table => new
                {
                    TravelListItemImageID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TravelListItemID = table.Column<int>(nullable: false),
                    ImageData = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelListImages", x => x.TravelListItemImageID);
                });

            migrationBuilder.CreateTable(
                name: "TravelLists",
                columns: table => new
                {
                    TravelListItemID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Latitude = table.Column<decimal>(nullable: false),
                    Longitude = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelLists", x => x.TravelListItemID);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    CheckListItemID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    TravelListItemID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.CheckListItemID);
                    table.ForeignKey(
                        name: "FK_Items_TravelLists_TravelListItemID",
                        column: x => x.TravelListItemID,
                        principalTable: "TravelLists",
                        principalColumn: "TravelListItemID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Points",
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
                    table.PrimaryKey("PK_Points", x => x.TravelPointOfInterestID);
                    table.ForeignKey(
                        name: "FK_Points_TravelLists_TravelListItemID",
                        column: x => x.TravelListItemID,
                        principalTable: "TravelLists",
                        principalColumn: "TravelListItemID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_TravelListItemID",
                table: "Items",
                column: "TravelListItemID");

            migrationBuilder.CreateIndex(
                name: "IX_Points_TravelListItemID",
                table: "Points",
                column: "TravelListItemID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Points");

            migrationBuilder.DropTable(
                name: "TravelListImages");

            migrationBuilder.DropTable(
                name: "TravelLists");
        }
    }
}
