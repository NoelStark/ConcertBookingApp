using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConcertBookingApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initialzing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Concerts",
                columns: table => new
                {
                    ConcertId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsFavorite = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Concerts", x => x.ConcertId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Performances",
                columns: table => new
                {
                    PerformanceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConcertId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalSeats = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    AvailableSeats = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Performances", x => x.PerformanceId);
                    table.ForeignKey(
                        name: "FK_Performances_Concerts_ConcertId",
                        column: x => x.ConcertId,
                        principalTable: "Concerts",
                        principalColumn: "ConcertId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_Bookings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingPerformances",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    PerformanceId = table.Column<int>(type: "int", nullable: false),
                    SeatsBooked = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingPerformances", x => new { x.BookingId, x.PerformanceId });
                    table.ForeignKey(
                        name: "FK_BookingPerformances_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "BookingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingPerformances_Performances_PerformanceId",
                        column: x => x.PerformanceId,
                        principalTable: "Performances",
                        principalColumn: "PerformanceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Concerts",
                columns: new[] { "ConcertId", "Description", "Genre", "ImageUrl", "IsFavorite", "Name" },
                values: new object[,]
                {
                    { 1, "A high-energy event celebrating chart-topping hits and electrifying performances by popular pop artists.", "Pop", "pop.png", false, "Pop Pulse Festival" },
                    { 2, "A vibrant concert featuring a mix of iconic pop hits and fresh, emerging talent under dazzling lights.", "Jazz", "testconcert.png", false, "Starlight Pop Jazz" },
                    { 3, "An enchanting evening of timeless symphonies and masterful compositions performed by world-renowned orchestras and soloists.", "Classical", "classical.jpg", false, "Classical" },
                    { 4, "An electrifying night filled with pulsating beats, mesmerizing light shows, and high-energy performances by top EDM DJs.", "EDM", "edm.jpg", false, "Electric Vibes Festival" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "Name" },
                values: new object[,]
                {
                    { 1, "johndoe@example.com", "John Doe" },
                    { 2, "janesmith@example.com", "Jane Smith" },
                    { 3, "alicejohnson@example.com", "Alice Johnson" },
                    { 4, "bobbrown@example.com", "Bob Brown" }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "BookingId", "BookingDate", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 12, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 3, new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 4, new DateTime(2025, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 }
                });

            migrationBuilder.InsertData(
                table: "Performances",
                columns: new[] { "PerformanceId", "AvailableSeats", "ConcertId", "Date", "Location", "Price", "TotalSeats" },
                values: new object[,]
                {
                    { 1, 98, 1, new DateTime(2024, 12, 14, 12, 0, 0, 0, DateTimeKind.Unspecified), "Aspvägen", 100.0, 100 },
                    { 2, 143, 1, new DateTime(2024, 12, 15, 14, 0, 0, 0, DateTimeKind.Unspecified), "Aspvägen", 200.0, 150 },
                    { 3, 199, 1, new DateTime(2024, 12, 16, 16, 0, 0, 0, DateTimeKind.Unspecified), "Aspvägen", 300.0, 200 },
                    { 4, 100, 2, new DateTime(2024, 10, 12, 15, 0, 0, 0, DateTimeKind.Unspecified), "Gökgatan", 100.0, 100 },
                    { 6, 150, 2, new DateTime(2024, 10, 13, 13, 0, 0, 0, DateTimeKind.Unspecified), "Gökgatan", 200.0, 150 },
                    { 7, 200, 2, new DateTime(2024, 10, 14, 17, 0, 0, 0, DateTimeKind.Unspecified), "Gökgatan", 300.0, 200 },
                    { 8, 100, 3, new DateTime(2025, 1, 2, 20, 0, 0, 0, DateTimeKind.Unspecified), "Solvägen", 100.0, 100 },
                    { 9, 150, 3, new DateTime(2025, 1, 3, 21, 0, 0, 0, DateTimeKind.Unspecified), "Solvägen", 200.0, 150 },
                    { 10, 200, 3, new DateTime(2025, 1, 4, 22, 0, 0, 0, DateTimeKind.Unspecified), "Solvägen", 300.0, 200 },
                    { 11, 295, 4, new DateTime(2025, 3, 10, 18, 0, 0, 0, DateTimeKind.Unspecified), "Höstvägen", 150.0, 300 },
                    { 12, 395, 4, new DateTime(2025, 3, 11, 19, 0, 0, 0, DateTimeKind.Unspecified), "Höstvägen", 180.0, 400 },
                    { 13, 496, 4, new DateTime(2025, 3, 12, 20, 0, 0, 0, DateTimeKind.Unspecified), "Höstvägen", 200.0, 500 }
                });

            migrationBuilder.InsertData(
                table: "BookingPerformances",
                columns: new[] { "BookingId", "PerformanceId", "SeatsBooked" },
                values: new object[,]
                {
                    { 1, 1, 2 },
                    { 1, 2, 3 },
                    { 2, 2, 4 },
                    { 2, 3, 1 },
                    { 3, 11, 5 },
                    { 3, 12, 2 },
                    { 4, 12, 3 },
                    { 4, 13, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingPerformances_PerformanceId",
                table: "BookingPerformances",
                column: "PerformanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Performances_ConcertId",
                table: "Performances",
                column: "ConcertId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserId",
                table: "Users",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingPerformances");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Performances");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Concerts");
        }
    }
}
