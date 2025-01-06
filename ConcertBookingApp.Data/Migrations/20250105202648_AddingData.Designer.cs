﻿// <auto-generated />
using System;
using ConcertBookingApp.Data.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ConcertBookingApp.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250105202648_AddingData")]

    partial class AddingData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SharedResources.Models.Booking", b =>
                {
                    b.Property<int>("BookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookingId"));

                    b.Property<DateTime>("BookingDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("BookingId");

                    b.HasIndex("UserId");

                    b.ToTable("Bookings");

                    b.HasData(
                        new
                        {
                            BookingId = 1,
                            BookingDate = new DateTime(2024, 12, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = 1
                        });
                });

            modelBuilder.Entity("SharedResources.Models.BookingPerformance", b =>
                {
                    b.Property<int>("BookingId")
                        .HasColumnType("int");

                    b.Property<int>("PerformanceId")
                        .HasColumnType("int");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SeatsBooked")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BookingId", "PerformanceId");

                    b.HasIndex("PerformanceId");

                    b.ToTable("BookingPerformances");

                    b.HasData(
                        new
                        {
                            BookingId = 1,
                            PerformanceId = 1,
                            Genre = "",
                            ImageURL = "",
                            SeatsBooked = 0,
                            Title = ""
                        });
                });

            modelBuilder.Entity("SharedResources.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("ImageSource")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsSelected")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("SharedResources.Models.Concert", b =>
                {
                    b.Property<int>("ConcertId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ConcertId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsFavorite")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ConcertId");

                    b.ToTable("Concerts");

                    b.HasData(
                        new
                        {
                            ConcertId = 1,
                            Description = "A high-energy event celebrating chart-topping hits and electrifying performances by popular pop artists.",
                            Genre = "Pop",
                            ImageUrl = "edm.png",
                            IsFavorite = false,
                            Name = "Pop Pulse Festival"
                        },
                        new
                        {
                            ConcertId = 2,
                            Description = "A vibrant concert featuring a mix of iconic pop hits and fresh, emerging talent under dazzling lights.",
                            Genre = "Jazz",
                            ImageUrl = "testconcert.png",
                            IsFavorite = false,
                            Name = "Starlight Pop Jazz"
                        },
                        new
                        {
                            ConcertId = 3,
                            Description = "A vibrant concert featuring a mix of iconic pop hits and fresh, emerging talent under dazzling lights.",
                            Genre = "Classical",
                            ImageUrl = "edm.png",
                            IsFavorite = false,
                            Name = "Classical"
                        });
                });

            modelBuilder.Entity("SharedResources.Models.Performance", b =>
                {
                    b.Property<int>("PerformanceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PerformanceId"));

                    b.Property<int>("AvailableSeats")
                        .HasColumnType("int");

                    b.Property<int>("ConcertId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("TotalSeats")
                        .HasColumnType("int");

                    b.HasKey("PerformanceId");

                    b.HasIndex("ConcertId");

                    b.ToTable("Performances");

                    b.HasData(
                        new
                        {
                            PerformanceId = 1,
                            AvailableSeats = 5,
                            ConcertId = 1,
                            Date = new DateTime(2024, 12, 14, 12, 0, 0, 0, DateTimeKind.Unspecified),
                            Location = "Aspvägen",
                            Price = 100.0,
                            TotalSeats = 5
                        },
                        new
                        {
                            PerformanceId = 2,
                            AvailableSeats = 150,
                            ConcertId = 1,
                            Date = new DateTime(2024, 12, 15, 14, 0, 0, 0, DateTimeKind.Unspecified),
                            Location = "Aspvägen",
                            Price = 200.0,
                            TotalSeats = 150
                        },
                        new
                        {
                            PerformanceId = 3,
                            AvailableSeats = 200,
                            ConcertId = 1,
                            Date = new DateTime(2024, 12, 16, 16, 0, 0, 0, DateTimeKind.Unspecified),
                            Location = "Aspvägen",
                            Price = 300.0,
                            TotalSeats = 200
                        },
                        new
                        {
                            PerformanceId = 4,
                            AvailableSeats = 5,
                            ConcertId = 2,
                            Date = new DateTime(2024, 10, 12, 15, 0, 0, 0, DateTimeKind.Unspecified),
                            Location = "Aspvägen",
                            Price = 100.0,
                            TotalSeats = 5
                        },
                        new
                        {
                            PerformanceId = 6,
                            AvailableSeats = 150,
                            ConcertId = 2,
                            Date = new DateTime(2024, 10, 13, 13, 0, 0, 0, DateTimeKind.Unspecified),
                            Location = "Aspvägen",
                            Price = 200.0,
                            TotalSeats = 150
                        },
                        new
                        {
                            PerformanceId = 7,
                            AvailableSeats = 200,
                            ConcertId = 2,
                            Date = new DateTime(2024, 10, 14, 17, 0, 0, 0, DateTimeKind.Unspecified),
                            Location = "Aspvägen",
                            Price = 300.0,
                            TotalSeats = 200
                        },
                        new
                        {
                            PerformanceId = 8,
                            AvailableSeats = 5,
                            ConcertId = 3,
                            Date = new DateTime(2025, 1, 2, 20, 0, 0, 0, DateTimeKind.Unspecified),
                            Location = "Aspvägen",
                            Price = 100.0,
                            TotalSeats = 5
                        },
                        new
                        {
                            PerformanceId = 9,
                            AvailableSeats = 150,
                            ConcertId = 3,
                            Date = new DateTime(2025, 1, 3, 21, 0, 0, 0, DateTimeKind.Unspecified),
                            Location = "Aspvägen",
                            Price = 200.0,
                            TotalSeats = 150
                        },
                        new
                        {
                            PerformanceId = 10,
                            AvailableSeats = 200,
                            ConcertId = 3,
                            Date = new DateTime(2025, 1, 4, 22, 0, 0, 0, DateTimeKind.Unspecified),
                            Location = "Aspvägen",
                            Price = 300.0,
                            TotalSeats = 200
                        });
                });

            modelBuilder.Entity("SharedResources.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Email = "johndoe@example.com",
                            Name = "John Doe"
                        },
                        new
                        {
                            UserId = 2,
                            Email = "janesmith@example.com",
                            Name = "Jane Smith"
                        });
                });

            modelBuilder.Entity("SharedResources.Models.Booking", b =>
                {
                    b.HasOne("SharedResources.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SharedResources.Models.BookingPerformance", b =>
                {
                    b.HasOne("SharedResources.Models.Booking", "Booking")
                        .WithMany("BookingPerformances")
                        .HasForeignKey("BookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SharedResources.Models.Performance", "Performance")
                        .WithMany("BookingPerformances")
                        .HasForeignKey("PerformanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Booking");

                    b.Navigation("Performance");
                });

            modelBuilder.Entity("SharedResources.Models.Performance", b =>
                {
                    b.HasOne("SharedResources.Models.Concert", null)
                        .WithMany("Performances")
                        .HasForeignKey("ConcertId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SharedResources.Models.Booking", b =>
                {
                    b.Navigation("BookingPerformances");
                });

            modelBuilder.Entity("SharedResources.Models.Concert", b =>
                {
                    b.Navigation("Performances");
                });

            modelBuilder.Entity("SharedResources.Models.Performance", b =>
                {
                    b.Navigation("BookingPerformances");
                });
#pragma warning restore 612, 618
        }
    }
}
