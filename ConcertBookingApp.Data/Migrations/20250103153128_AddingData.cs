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
                columns: new[] { "ConcertId", "Description", "Genre", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { 1, "Pop Pulse Festival", "Pop", "edm.png", "Pop Pulse Festival" },
                    { 2, "Starlight Pop Jazz", "Jazz", "testconcert.png", "Starlight Pop Jazz" },
                    { 3, "Classical", "Classical", "edm.png", "Classical" }
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
                columns: new[] { "PerformanceId", "ConcertId", "Date", "Location", "Price", "TotalSeats" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 12, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aspvägen", 100.0, 5 },
                    { 2, 1, new DateTime(2024, 12, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aspvägen", 200.0, 150 },
                    { 3, 1, new DateTime(2024, 12, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aspvägen", 300.0, 200 },
                    { 4, 2, new DateTime(2024, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aspvägen", 100.0, 5 },
                    { 6, 2, new DateTime(2024, 7, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aspvägen", 200.0, 150 },
                    { 7, 2, new DateTime(2024, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aspvägen", 300.0, 200 },
                    { 8, 3, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aspvägen", 100.0, 5 },
                    { 9, 3, new DateTime(2024, 12, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aspvägen", 200.0, 150 },
                    { 10, 3, new DateTime(2024, 12, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aspvägen", 300.0, 200 }
                });

            migrationBuilder.InsertData(
                table: "BookingPerformances",
                columns: new[] { "BookingId", "PerformanceId" },
                values: new object[] { 1, 1 });
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
