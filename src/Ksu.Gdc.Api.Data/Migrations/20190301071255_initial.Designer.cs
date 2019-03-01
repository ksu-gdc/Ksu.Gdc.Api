﻿// <auto-generated />
using System;
using Ksu.Gdc.Api.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Ksu.Gdc.Api.Data.Migrations
{
    [DbContext(typeof(KsuGdcContext))]
    [Migration("20190301071255_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Ksu.Gdc.Api.Data.Entities.DbEntity_Game", b =>
                {
                    b.Property<int>("GameId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(1000);

                    b.Property<string>("HostUrl")
                        .HasMaxLength(2000);

                    b.Property<bool>("IsFeatured")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("GameId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Ksu.Gdc.Api.Data.Entities.DbEntity_GameImage", b =>
                {
                    b.Property<int>("GameId");

                    b.Property<int>("ImageId");

                    b.HasKey("GameId", "ImageId");

                    b.HasIndex("ImageId");

                    b.ToTable("GameImages");
                });

            modelBuilder.Entity("Ksu.Gdc.Api.Data.Entities.DbEntity_GameUser", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("GameId");

                    b.HasKey("UserId", "GameId");

                    b.HasIndex("GameId");

                    b.ToTable("GameUsers");
                });

            modelBuilder.Entity("Ksu.Gdc.Api.Data.Entities.DbEntity_Image", b =>
                {
                    b.Property<int>("ImageId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<byte[]>("Data")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("ImageId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Ksu.Gdc.Api.Data.Entities.DbEntity_Officer", b =>
                {
                    b.Property<int>("OfficerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int?>("UserId");

                    b.HasKey("OfficerId");

                    b.HasIndex("UserId");

                    b.ToTable("Officers");
                });

            modelBuilder.Entity("Ksu.Gdc.Api.Data.Entities.DbEntity_User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(500);

                    b.Property<string>("FirstName")
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .HasMaxLength(50);

                    b.Property<string>("Username")
                        .IsRequired();

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Ksu.Gdc.Api.Data.Entities.DbEntity_UserImage", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("ImageId");

                    b.HasKey("UserId", "ImageId");

                    b.HasIndex("ImageId");

                    b.ToTable("UserImages");
                });

            modelBuilder.Entity("Ksu.Gdc.Api.Data.Entities.DbEntity_GameImage", b =>
                {
                    b.HasOne("Ksu.Gdc.Api.Data.Entities.DbEntity_Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Ksu.Gdc.Api.Data.Entities.DbEntity_Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Ksu.Gdc.Api.Data.Entities.DbEntity_GameUser", b =>
                {
                    b.HasOne("Ksu.Gdc.Api.Data.Entities.DbEntity_Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Ksu.Gdc.Api.Data.Entities.DbEntity_User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Ksu.Gdc.Api.Data.Entities.DbEntity_Officer", b =>
                {
                    b.HasOne("Ksu.Gdc.Api.Data.Entities.DbEntity_User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Ksu.Gdc.Api.Data.Entities.DbEntity_UserImage", b =>
                {
                    b.HasOne("Ksu.Gdc.Api.Data.Entities.DbEntity_Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Ksu.Gdc.Api.Data.Entities.DbEntity_User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
