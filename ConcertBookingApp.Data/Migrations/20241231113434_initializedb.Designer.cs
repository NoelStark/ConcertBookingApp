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
    [Migration("20241231113434_initializedb")]
    partial class initializedb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ConcertBookingApp.Data.Models.Booking", b =>
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
                });

            modelBuilder.Entity("ConcertBookingApp.Data.Models.BookingPerformance", b =>
                {
                    b.Property<int>("BookingId")
                        .HasColumnType("int");

                    b.Property<int>("PerformanceId")
                        .HasColumnType("int");

                    b.HasKey("BookingId");

                    b.HasIndex("PerformanceId")
                        .IsUnique();

                    b.ToTable("BookingPerformances");
                });

            modelBuilder.Entity("ConcertBookingApp.Data.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("ImageSource")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("ConcertBookingApp.Data.Models.Concert", b =>
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

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ConcertId");

                    b.ToTable("Concerts");
                });

            modelBuilder.Entity("ConcertBookingApp.Data.Models.Performance", b =>
                {
                    b.Property<int>("PerformanceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PerformanceId"));

                    b.Property<int?>("BookingId")
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

                    b.HasIndex("BookingId");

                    b.HasIndex("ConcertId");

                    b.ToTable("Performances");
                });

            modelBuilder.Entity("ConcertBookingApp.Data.Models.User", b =>
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
                });

            modelBuilder.Entity("ConcertBookingApp.Data.Models.Booking", b =>
                {
                    b.HasOne("ConcertBookingApp.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ConcertBookingApp.Data.Models.BookingPerformance", b =>
                {
                    b.HasOne("ConcertBookingApp.Data.Models.Booking", "Booking")
                        .WithMany("BookingPerformances")
                        .HasForeignKey("BookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConcertBookingApp.Data.Models.Performance", "Performance")
                        .WithOne("BookingPerformance")
                        .HasForeignKey("ConcertBookingApp.Data.Models.BookingPerformance", "PerformanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Booking");

                    b.Navigation("Performance");
                });

            modelBuilder.Entity("ConcertBookingApp.Data.Models.Performance", b =>
                {
                    b.HasOne("ConcertBookingApp.Data.Models.Booking", null)
                        .WithMany("Performances")
                        .HasForeignKey("BookingId");

                    b.HasOne("ConcertBookingApp.Data.Models.Concert", "Concert")
                        .WithMany("Performances")
                        .HasForeignKey("ConcertId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Concert");
                });

            modelBuilder.Entity("ConcertBookingApp.Data.Models.Booking", b =>
                {
                    b.Navigation("BookingPerformances");

                    b.Navigation("Performances");
                });

            modelBuilder.Entity("ConcertBookingApp.Data.Models.Concert", b =>
                {
                    b.Navigation("Performances");
                });

            modelBuilder.Entity("ConcertBookingApp.Data.Models.Performance", b =>
                {
                    b.Navigation("BookingPerformance")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
