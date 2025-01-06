using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConcertBookingApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Concerts",
                columns: new[] { "ConcertId", "Description", "Genre", "ImageUrl", "IsFavorite", "Name" },
                values: new object[,]
                {
                    { 1, "A high-energy event celebrating chart-topping hits and electrifying performances by popular pop artists.", "Pop", "edm.png", false, "Pop Pulse Festival" },
                    { 2, "A vibrant concert featuring a mix of iconic pop hits and fresh, emerging talent under dazzling lights.", "Jazz", "testconcert.png", false, "Starlight Pop Jazz" },
                    { 3, "A vibrant concert featuring a mix of iconic pop hits and fresh, emerging talent under dazzling lights.", "Classical", "edm.png", false, "Classical" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "Name" },
                values: new object[,]
                {
                    { 1, "johndoe@example.com", "John Doe" },
                    { 2, "janesmith@example.com", "Jane Smith" }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "BookingId", "BookingDate", "UserId" },
                values: new object[] { 1, new DateTime(2024, 12, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.InsertData(
                table: "Performances",
                columns: new[] { "PerformanceId", "AvailableSeats", "ConcertId", "Date", "Location", "Price", "TotalSeats" },
                values: new object[,]
                {
<<<<<<<< HEAD:ConcertBookingApp.Data/Migrations/20250105202648_AddingData.cs
                    { 1, 5, 1, new DateTime(2024, 12, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aspvägen", 100.0, 5 },
                    { 2, 150, 1, new DateTime(2024, 12, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aspvägen", 200.0, 150 },
                    { 3, 200, 1, new DateTime(2024, 12, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aspvägen", 300.0, 200 },
                    { 4, 5, 2, new DateTime(2024, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aspvägen", 100.0, 5 },
                    { 6, 150, 2, new DateTime(2024, 7, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aspvägen", 200.0, 150 },
                    { 7, 200, 2, new DateTime(2024, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aspvägen", 300.0, 200 },
                    { 8, 5, 3, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aspvägen", 100.0, 5 },
                    { 9, 150, 3, new DateTime(2024, 12, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aspvägen", 200.0, 150 },
                    { 10, 200, 3, new DateTime(2024, 12, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aspvägen", 300.0, 200 }
========
                    { 1, 5, 1, new DateTime(2024, 12, 14, 12, 0, 0, 0, DateTimeKind.Unspecified), "Aspvägen", 100.0, 5 },
                    { 2, 150, 1, new DateTime(2024, 12, 15, 14, 0, 0, 0, DateTimeKind.Unspecified), "Aspvägen", 200.0, 150 },
                    { 3, 200, 1, new DateTime(2024, 12, 16, 16, 0, 0, 0, DateTimeKind.Unspecified), "Aspvägen", 300.0, 200 },
                    { 4, 5, 2, new DateTime(2024, 10, 12, 15, 0, 0, 0, DateTimeKind.Unspecified), "Aspvägen", 100.0, 5 },
                    { 6, 150, 2, new DateTime(2024, 10, 13, 13, 0, 0, 0, DateTimeKind.Unspecified), "Aspvägen", 200.0, 150 },
                    { 7, 200, 2, new DateTime(2024, 10, 14, 17, 0, 0, 0, DateTimeKind.Unspecified), "Aspvägen", 300.0, 200 },
                    { 8, 5, 3, new DateTime(2025, 1, 2, 20, 0, 0, 0, DateTimeKind.Unspecified), "Aspvägen", 100.0, 5 },
                    { 9, 150, 3, new DateTime(2025, 1, 3, 21, 0, 0, 0, DateTimeKind.Unspecified), "Aspvägen", 200.0, 150 },
                    { 10, 200, 3, new DateTime(2025, 1, 4, 22, 0, 0, 0, DateTimeKind.Unspecified), "Aspvägen", 300.0, 200 }
>>>>>>>> Noel:ConcertBookingApp.Data/Migrations/20250106112045_AddingData.cs
                });

            migrationBuilder.InsertData(
                table: "BookingPerformances",
                columns: new[] { "BookingId", "PerformanceId", "Genre", "ImageURL", "SeatsBooked", "Title" },
                values: new object[] { 1, 1, "", "", 0, "" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BookingPerformances",
                keyColumns: new[] { "BookingId", "PerformanceId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Performances",
                keyColumn: "PerformanceId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Performances",
                keyColumn: "PerformanceId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Performances",
                keyColumn: "PerformanceId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Performances",
                keyColumn: "PerformanceId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Performances",
                keyColumn: "PerformanceId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Performances",
                keyColumn: "PerformanceId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Performances",
                keyColumn: "PerformanceId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Performances",
                keyColumn: "PerformanceId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Concerts",
                keyColumn: "ConcertId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Concerts",
                keyColumn: "ConcertId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Performances",
                keyColumn: "PerformanceId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Concerts",
                keyColumn: "ConcertId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);
        }
    }
}
