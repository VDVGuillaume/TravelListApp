﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TravelListRepository.Sql;

namespace TravelList.Api.Migrations
{
    [DbContext(typeof(TravelListContext))]
    [Migration("20201230101040_Migration v2.0")]
    partial class Migrationv20
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TravelListModels.CheckListItem", b =>
                {
                    b.Property<int>("CheckListItemID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int>("TravelListItemID");

                    b.HasKey("CheckListItemID");

                    b.HasIndex("TravelListItemID");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("TravelListModels.TravelListItem", b =>
                {
                    b.Property<int>("TravelListItemID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Country");

                    b.Property<string>("Description");

                    b.Property<DateTime>("EndDate");

                    b.Property<decimal>("Latitude");

                    b.Property<decimal>("Longitude");

                    b.Property<string>("Name");

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("UserId");

                    b.HasKey("TravelListItemID");

                    b.ToTable("TravelLists");
                });

            modelBuilder.Entity("TravelListModels.TravelListItemImage", b =>
                {
                    b.Property<int>("TravelListItemImageID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("ImageData");

                    b.Property<string>("ImageName");

                    b.Property<int>("TravelListItemID");

                    b.HasKey("TravelListItemImageID");

                    b.HasIndex("TravelListItemID");

                    b.ToTable("TravelListImages");
                });

            modelBuilder.Entity("TravelListModels.TravelPointOfInterest", b =>
                {
                    b.Property<int>("TravelPointOfInterestID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Latitude");

                    b.Property<decimal>("Longitude");

                    b.Property<string>("Name");

                    b.Property<int>("TravelListItemID");

                    b.HasKey("TravelPointOfInterestID");

                    b.HasIndex("TravelListItemID");

                    b.ToTable("Points");
                });

            modelBuilder.Entity("TravelListModels.TravelRoute", b =>
                {
                    b.Property<int>("TravelRouteID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EndTravelPointOfInterestID");

                    b.Property<int>("StartTravelPointOfInterestID");

                    b.Property<int>("TravelListItemID");

                    b.HasKey("TravelRouteID");

                    b.HasIndex("TravelListItemID");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("TravelListModels.CheckListItem", b =>
                {
                    b.HasOne("TravelListModels.TravelListItem")
                        .WithMany("Items")
                        .HasForeignKey("TravelListItemID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TravelListModels.TravelListItemImage", b =>
                {
                    b.HasOne("TravelListModels.TravelListItem")
                        .WithMany("Images")
                        .HasForeignKey("TravelListItemID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TravelListModels.TravelPointOfInterest", b =>
                {
                    b.HasOne("TravelListModels.TravelListItem")
                        .WithMany("Points")
                        .HasForeignKey("TravelListItemID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TravelListModels.TravelRoute", b =>
                {
                    b.HasOne("TravelListModels.TravelListItem")
                        .WithMany("Routes")
                        .HasForeignKey("TravelListItemID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
