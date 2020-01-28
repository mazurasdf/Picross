﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Picross.Contexts;

namespace Picross.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20200128201821_FixEmailLoginUser")]
    partial class FixEmailLoginUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Picross.Models.Like", b =>
                {
                    b.Property<int>("LikeId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("PuzzleId");

                    b.Property<int>("UserId");

                    b.HasKey("LikeId");

                    b.HasIndex("PuzzleId");

                    b.HasIndex("UserId");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("Picross.Models.Puzzle", b =>
                {
                    b.Property<int>("PuzzleId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<int>("UserId");

                    b.Property<string>("puzzleData")
                        .IsRequired();

                    b.Property<int>("xSize")
                        .HasMaxLength(40);

                    b.Property<int>("ySize")
                        .HasMaxLength(40);

                    b.HasKey("PuzzleId");

                    b.HasIndex("UserId");

                    b.ToTable("Puzzles");
                });

            modelBuilder.Entity("Picross.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<string>("UserName")
                        .IsRequired();

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Picross.Models.Like", b =>
                {
                    b.HasOne("Picross.Models.Puzzle", "LikedPuzzle")
                        .WithMany("LikesReceived")
                        .HasForeignKey("PuzzleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Picross.Models.User", "LikedByUser")
                        .WithMany("LikesGiven")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Picross.Models.Puzzle", b =>
                {
                    b.HasOne("Picross.Models.User", "Creator")
                        .WithMany("CreatedPuzzles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
