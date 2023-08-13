﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NZwalks.API.Data;

#nullable disable

namespace NZwalks.API.Migrations
{
    [DbContext(typeof(NZWalksDbContext))]
    [Migration("20230720052003_Seeding Difficulty and Region")]
    partial class SeedingDifficultyandRegion
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NZwalks.API.Models.Domain.Difficulty", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Difficulties");

                    b.HasData(
                        new
                        {
                            id = new Guid("d959df6c-e8ef-4aa7-b4b7-cea3f608bf9a"),
                            Name = "Easy"
                        },
                        new
                        {
                            id = new Guid("6b11cf7d-65ce-4a54-a1f8-ad9596774ea7"),
                            Name = "Medium"
                        },
                        new
                        {
                            id = new Guid("e400333f-b2e5-4967-843c-f09690a0cc58"),
                            Name = "Hard"
                        });
                });

            modelBuilder.Entity("NZwalks.API.Models.Domain.Region", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegionImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Regions");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c2e0849a-53f8-432f-840b-bca4aba3158f"),
                            Code = "MUB",
                            Name = "Mumbai",
                            RegionImageUrl = "https://cdn.britannica.com/26/84526-050-45452C37/Gateway-monument-India-entrance-Mumbai-Harbour-coast.jpg"
                        },
                        new
                        {
                            Id = new Guid("4a921308-71d5-4d7a-a842-b5c493f9ff8e"),
                            Code = "DL",
                            Name = "Delhi",
                            RegionImageUrl = "https://cdn.britannica.com/37/189837-050-F0AF383E/New-Delhi-India-War-Memorial-arch-Sir.jpg"
                        },
                        new
                        {
                            Id = new Guid("3f9dc9bd-c7b4-4000-8553-cb24e49ab959"),
                            Code = "LKO",
                            Name = "Lucknow",
                            RegionImageUrl = "https://ghoomophiro.com/wp-content/uploads/2022/10/Places-to-visit-in-lucknow-scaled.jpg"
                        },
                        new
                        {
                            Id = new Guid("eb9775e6-ba49-4e46-81e0-503cc751ca3f"),
                            Code = "PUN",
                            Name = "Pune",
                            RegionImageUrl = "https://mittalbuilders.com/wp-content/uploads/2020/12/Reasons-to-settle-down-in-Pune-1400x700.png"
                        });
                });

            modelBuilder.Entity("NZwalks.API.Models.Domain.Walk", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("DifficultyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("LengthInKm")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RegionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("WalkImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DifficultyId");

                    b.HasIndex("RegionId");

                    b.ToTable("Walks");
                });

            modelBuilder.Entity("NZwalks.API.Models.Domain.Walk", b =>
                {
                    b.HasOne("NZwalks.API.Models.Domain.Difficulty", "Difficulty")
                        .WithMany()
                        .HasForeignKey("DifficultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NZwalks.API.Models.Domain.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Difficulty");

                    b.Navigation("Region");
                });
#pragma warning restore 612, 618
        }
    }
}
