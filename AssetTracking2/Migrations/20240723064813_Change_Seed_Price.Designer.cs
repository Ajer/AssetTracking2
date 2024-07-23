﻿// <auto-generated />
using System;
using AssetTracking2.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AssetTracking2.Migrations
{
    [DbContext(typeof(AssetContext))]
    [Migration("20240723064813_Change_Seed_Price")]
    partial class Change_Seed_Price
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AssetTracking2.Models.Asset", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Brand")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Model")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("OfficeId")
                        .HasColumnType("int");

                    b.Property<double?>("PriceInDollar")
                        .HasColumnType("float");

                    b.Property<DateTime?>("PurchaseDate")
                        .HasMaxLength(50)
                        .HasColumnType("datetime2");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.HasIndex("OfficeId");

                    b.ToTable("Assets");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("AssetTracking2.Models.Office", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Country")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Offices");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            City = "Stockholm",
                            Country = "Sweden"
                        },
                        new
                        {
                            Id = 2,
                            City = "Buffalo",
                            Country = "USA"
                        },
                        new
                        {
                            Id = 3,
                            City = "Madrid",
                            Country = "Spain"
                        });
                });

            modelBuilder.Entity("AssetTracking2.Models.Computer", b =>
                {
                    b.HasBaseType("AssetTracking2.Models.Asset");

                    b.ToTable("Computers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Brand = "Dell",
                            Model = "XPS",
                            OfficeId = 1,
                            PriceInDollar = 524.0,
                            PurchaseDate = new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Type = "Computer"
                        },
                        new
                        {
                            Id = 2,
                            Brand = "MacBook",
                            Model = "Air",
                            OfficeId = 2,
                            PriceInDollar = 654.0,
                            PurchaseDate = new DateTime(2023, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Type = "Computer"
                        });
                });

            modelBuilder.Entity("AssetTracking2.Models.Phone", b =>
                {
                    b.HasBaseType("AssetTracking2.Models.Asset");

                    b.ToTable("Phones");

                    b.HasData(
                        new
                        {
                            Id = 3,
                            Brand = "Iphone",
                            Model = "8",
                            OfficeId = 2,
                            PriceInDollar = 298.0,
                            PurchaseDate = new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Type = "Phone"
                        },
                        new
                        {
                            Id = 4,
                            Brand = "Samsung",
                            Model = "fold",
                            OfficeId = 1,
                            PriceInDollar = 241.0,
                            PurchaseDate = new DateTime(2023, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Type = "Phone"
                        });
                });

            modelBuilder.Entity("AssetTracking2.Models.Asset", b =>
                {
                    b.HasOne("AssetTracking2.Models.Office", "Office")
                        .WithMany("Assets")
                        .HasForeignKey("OfficeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Office");
                });

            modelBuilder.Entity("AssetTracking2.Models.Computer", b =>
                {
                    b.HasOne("AssetTracking2.Models.Asset", null)
                        .WithOne()
                        .HasForeignKey("AssetTracking2.Models.Computer", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AssetTracking2.Models.Phone", b =>
                {
                    b.HasOne("AssetTracking2.Models.Asset", null)
                        .WithOne()
                        .HasForeignKey("AssetTracking2.Models.Phone", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AssetTracking2.Models.Office", b =>
                {
                    b.Navigation("Assets");
                });
#pragma warning restore 612, 618
        }
    }
}