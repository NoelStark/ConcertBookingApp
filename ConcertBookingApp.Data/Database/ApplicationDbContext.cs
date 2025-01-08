using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedResources.Models;
using Microsoft.Extensions.Configuration;

namespace ConcertBookingApp.Data.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingPerformance> BookingPerformances { get; set; }
        public DbSet<Performance> Performances { get; set; }
        public DbSet<Concert> Concerts { get; set; }
        public DbSet<User> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Concert>()
         .HasMany(c => c.Performances)
         .WithOne()
         .HasForeignKey(p => p.ConcertId);

            modelBuilder.Entity<BookingPerformance>()
                .Ignore(x => x.Genre)
                .Ignore(x => x.Title)
                .Ignore(x => x.ImageURL)
                .HasKey(bp => new { bp.BookingId, bp.PerformanceId });

        
            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserId)
                .IsUnique();

            modelBuilder.Entity<Booking>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(b => b.UserId);

            modelBuilder.Entity<Concert>().HasData(
                new Concert { ConcertId = 1, Description = "A high-energy event celebrating chart-topping hits and electrifying performances by popular pop artists.", Genre = "Pop", ImageUrl = "pop.png", Name = "Pop Pulse Festival" },
                new Concert { ConcertId = 2, Description = "A vibrant concert featuring a mix of iconic pop hits and fresh, emerging talent under dazzling lights.", Genre = "Jazz", ImageUrl = "testconcert.png", Name = "Starlight Pop Jazz" },
                new Concert { ConcertId = 3, Description = "An enchanting evening of timeless symphonies and masterful compositions performed by world-renowned orchestras and soloists.", Genre = "Classical", ImageUrl = "classical.jpg", Name = "Classical" },
                new Concert { ConcertId = 4, Description = "An electrifying night filled with pulsating beats, mesmerizing light shows, and high-energy performances by top EDM DJs.", Genre = "EDM", ImageUrl = "edm.jpg", Name = "Electric Vibes Festival" }
            );
            modelBuilder.Entity<Performance>().HasData(
                new Performance { PerformanceId = 1, ConcertId = 1, TotalSeats = 100, Date = DateTime.Parse("2024-12-14 12:00"), Location = "Aspvägen", Price = 100, AvailableSeats = 100 - 2 },
                new Performance { PerformanceId = 2, ConcertId = 1, TotalSeats = 150, Date = DateTime.Parse("2024-12-15 14:00"), Location = "Aspvägen", Price = 200, AvailableSeats = 150 - (3 + 4) },
                new Performance { PerformanceId = 3, ConcertId = 1, TotalSeats = 200, Date = DateTime.Parse("2024-12-16 16:00"), Location = "Aspvägen", Price = 300, AvailableSeats = 200 - 1 },
                new Performance { PerformanceId = 4, ConcertId = 2, TotalSeats = 100, Date = DateTime.Parse("2024-10-12 15:00"), Location = "Gökgatan", Price = 100, AvailableSeats = 100 },
                new Performance { PerformanceId = 6, ConcertId = 2, TotalSeats = 150, Date = DateTime.Parse("2024-10-13 13:00"), Location = "Gökgatan", Price = 200, AvailableSeats = 150 },
                new Performance { PerformanceId = 7, ConcertId = 2, TotalSeats = 200, Date = DateTime.Parse("2024-10-14 17:00"), Location = "Gökgatan", Price = 300, AvailableSeats = 200 },
                new Performance { PerformanceId = 8, ConcertId = 3, TotalSeats = 100, Date = DateTime.Parse("2025-01-02 20:00"), Location = "Solvägen", Price = 100, AvailableSeats = 100 },
                new Performance { PerformanceId = 9, ConcertId = 3, TotalSeats = 150, Date = DateTime.Parse("2025-01-03 21:00"), Location = "Solvägen", Price = 200, AvailableSeats = 150 },
                new Performance { PerformanceId = 10, ConcertId = 3, TotalSeats = 200, Date = DateTime.Parse("2025-01-04 22:00"), Location = "Solvägen", Price = 300, AvailableSeats = 200 },
                new Performance { PerformanceId = 11, ConcertId = 4, TotalSeats = 300, Date = DateTime.Parse("2025-03-10 18:00"), Location = "Höstvägen", Price = 150, AvailableSeats = 300 - 5 },
                new Performance { PerformanceId = 12, ConcertId = 4, TotalSeats = 400, Date = DateTime.Parse("2025-03-11 19:00"), Location = "Höstvägen", Price = 180, AvailableSeats = 400 - (2 + 3) },
                new Performance { PerformanceId = 13, ConcertId = 4, TotalSeats = 500, Date = DateTime.Parse("2025-03-12 20:00"), Location = "Höstvägen", Price = 200, AvailableSeats = 500 - 4 }
            );


            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, Name = "John Doe", Email = "johndoe@example.com" },
                new User { UserId = 2, Name = "Jane Smith", Email = "janesmith@example.com" },
                new User { UserId = 3, Name = "Alice Johnson", Email = "alicejohnson@example.com" },
                new User { UserId = 4, Name = "Bob Brown", Email = "bobbrown@example.com" }
            );

            modelBuilder.Entity<Booking>().HasData(
                new Booking { BookingId = 1, UserId = 1, BookingDate = DateTime.Parse("2024-12-14") },
                new Booking { BookingId = 2, UserId = 2, BookingDate = DateTime.Parse("2024-12-15") },
                new Booking { BookingId = 3, UserId = 3, BookingDate = DateTime.Parse("2025-03-10") },
                new Booking { BookingId = 4, UserId = 4, BookingDate = DateTime.Parse("2025-03-11") }
            );

            modelBuilder.Entity<BookingPerformance>().HasData(
                new BookingPerformance { BookingId = 1, PerformanceId = 1, SeatsBooked = 2 },
                new BookingPerformance { BookingId = 1, PerformanceId = 2, SeatsBooked = 3 },
                new BookingPerformance { BookingId = 2, PerformanceId = 2, SeatsBooked = 4 },
                new BookingPerformance { BookingId = 2, PerformanceId = 3, SeatsBooked = 1 },
                new BookingPerformance { BookingId = 3, PerformanceId = 11, SeatsBooked = 5 }, 
                new BookingPerformance { BookingId = 3, PerformanceId = 12, SeatsBooked = 2 },
                new BookingPerformance { BookingId = 4, PerformanceId = 12, SeatsBooked = 3 },
                new BookingPerformance { BookingId = 4, PerformanceId = 13, SeatsBooked = 4 }
            );


        }
    }
}
