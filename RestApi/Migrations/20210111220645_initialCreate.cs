using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelList.Api.Migrations
{
    public partial class initialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "TravelLists",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: true),
                    TravelListItemID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
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
                    TravelCheckListItemID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Amount = table.Column<int>(nullable: false),
                    Category = table.Column<string>(nullable: true),
                    Checked = table.Column<bool>(nullable: false),
                    TravelListItemID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.TravelCheckListItemID);
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

            migrationBuilder.CreateTable(
                name: "TravelListImages",
                columns: table => new
                {
                    TravelListItemImageID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TravelListItemID = table.Column<int>(nullable: false),
                    ImageData = table.Column<byte[]>(nullable: true),
                    ImageName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelListImages", x => x.TravelListItemImageID);
                    table.ForeignKey(
                        name: "FK_TravelListImages_TravelLists_TravelListItemID",
                        column: x => x.TravelListItemID,
                        principalTable: "TravelLists",
                        principalColumn: "TravelListItemID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    TravelRouteID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TravelListItemID = table.Column<int>(nullable: false),
                    Driving = table.Column<bool>(nullable: false),
                    StartTravelPointOfInterestID = table.Column<int>(nullable: true),
                    EndTravelPointOfInterestID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.TravelRouteID);
                    table.ForeignKey(
                        name: "FK_Routes_Points_EndTravelPointOfInterestID",
                        column: x => x.EndTravelPointOfInterestID,
                        principalTable: "Points",
                        principalColumn: "TravelPointOfInterestID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Routes_Points_StartTravelPointOfInterestID",
                        column: x => x.StartTravelPointOfInterestID,
                        principalTable: "Points",
                        principalColumn: "TravelPointOfInterestID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Routes_TravelLists_TravelListItemID",
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

            migrationBuilder.CreateIndex(
                name: "IX_Routes_EndTravelPointOfInterestID",
                table: "Routes",
                column: "EndTravelPointOfInterestID");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_StartTravelPointOfInterestID",
                table: "Routes",
                column: "StartTravelPointOfInterestID");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_TravelListItemID",
                table: "Routes",
                column: "TravelListItemID");

            migrationBuilder.CreateIndex(
                name: "IX_TravelListImages_TravelListItemID",
                table: "TravelListImages",
                column: "TravelListItemID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Routes");

            migrationBuilder.DropTable(
                name: "TravelListImages");

            migrationBuilder.DropTable(
                name: "Points");

            migrationBuilder.DropTable(
                name: "TravelLists");
        }
    }
}
