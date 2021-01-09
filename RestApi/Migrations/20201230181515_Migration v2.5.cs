﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelList.Api.Migrations
{
    public partial class Migrationv25 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Points_EndTravelPointOfInterestID",
                table: "Routes");

            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Points_StartTravelPointOfInterestID",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_Routes_EndTravelPointOfInterestID",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_Routes_StartTravelPointOfInterestID",
                table: "Routes");

            migrationBuilder.AddColumn<int>(
                name: "TravelPointOfInterestID",
                table: "Routes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Routes_TravelPointOfInterestID",
                table: "Routes",
                column: "TravelPointOfInterestID");

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Points_TravelPointOfInterestID",
                table: "Routes",
                column: "TravelPointOfInterestID",
                principalTable: "Points",
                principalColumn: "TravelPointOfInterestID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Points_TravelPointOfInterestID",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_Routes_TravelPointOfInterestID",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "TravelPointOfInterestID",
                table: "Routes");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_EndTravelPointOfInterestID",
                table: "Routes",
                column: "EndTravelPointOfInterestID");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_StartTravelPointOfInterestID",
                table: "Routes",
                column: "StartTravelPointOfInterestID");

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Points_EndTravelPointOfInterestID",
                table: "Routes",
                column: "EndTravelPointOfInterestID",
                principalTable: "Points",
                principalColumn: "TravelPointOfInterestID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Points_StartTravelPointOfInterestID",
                table: "Routes",
                column: "StartTravelPointOfInterestID",
                principalTable: "Points",
                principalColumn: "TravelPointOfInterestID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}