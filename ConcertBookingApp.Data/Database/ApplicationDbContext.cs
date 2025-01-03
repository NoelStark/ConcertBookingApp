using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConcertBookingApp.Data.Models;
using Microsoft.Extensions.Configuration;

namespace ConcertBookingApp.Data.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingPerformance> BookingPerformances { get; set; }
        public DbSet<Performance> Performances { get; set; }
        public DbSet<Concert> Concerts { get; set; }
        public DbSet<Category> Categories { get; set; }
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

            // Configure composite key for BookingPerformance
            modelBuilder.Entity<BookingPerformance>()
                .HasKey(bp => new { bp.BookingId, bp.PerformanceId });

            // Configure Booking -> BookingPerformance (One-to-Many)
            //modelBuilder.Entity<BookingPerformance>()
            //    .HasOne(bp => bp.Booking)
            //    .WithMany()
            //    .HasForeignKey(bp => bp.BookingId);

            //// Configure Performance -> BookingPerformance (One-to-Many)
            //modelBuilder.Entity<BookingPerformance>()
            //    .HasOne(bp => bp.Performance)
            //    .WithMany()
            //    .HasForeignKey(bp => bp.PerformanceId);


            // Unique index for UserId in User table
            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserId)
                .IsUnique();

            // Configure User -> Booking (One-to-Many)
            modelBuilder.Entity<Booking>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(b => b.UserId);

            modelBuilder.Entity<Concert>().HasData(
        new Concert { ConcertId = 1, Description = "Pop Pulse Festival", Genre = "Pop", ImageUrl = "edm.png", Name = "Pop Pulse Festival" },
        new Concert { ConcertId = 2, Description = "Starlight Pop Jazz", Genre = "Jazz", ImageUrl = "testconcert.png", Name = "Starlight Pop Jazz" },
        new Concert { ConcertId = 3, Description = "Classical", Genre = "Classical", ImageUrl = "edm.png", Name = "Classical" }
    );

            modelBuilder.Entity<Performance>().HasData(
                new Performance { PerformanceId = 1, ConcertId = 1, TotalSeats = 5, Date = DateTime.Parse("2024-12-14"), Location = "Aspvägen", Price = 100 },
                new Performance { PerformanceId = 2, ConcertId = 1, TotalSeats = 150, Date = DateTime.Parse("2024-12-14"), Location = "Aspvägen", Price = 200 },
                new Performance { PerformanceId = 3, ConcertId = 1, TotalSeats = 200, Date = DateTime.Parse("2024-12-14"), Location = "Aspvägen", Price = 300 },
                new Performance { PerformanceId = 4, ConcertId = 2, TotalSeats = 5, Date = DateTime.Parse("2024-10-12"), Location = "Aspvägen", Price = 100 },
                new Performance { PerformanceId = 6, ConcertId = 2, TotalSeats = 150, Date = DateTime.Parse("2024-07-04"), Location = "Aspvägen", Price = 200 },
                new Performance { PerformanceId = 7, ConcertId = 2, TotalSeats = 200, Date = DateTime.Parse("2024-10-13"), Location = "Aspvägen", Price = 300 },
                new Performance { PerformanceId = 8, ConcertId = 3, TotalSeats = 5, Date = DateTime.Parse("2024-01-02"), Location = "Aspvägen", Price = 100 },
                new Performance { PerformanceId = 9, ConcertId = 3, TotalSeats = 150, Date = DateTime.Parse("2024-12-14"), Location = "Aspvägen", Price = 200 },
                new Performance { PerformanceId = 10, ConcertId = 3, TotalSeats = 200, Date = DateTime.Parse("2024-12-14"), Location = "Aspvägen", Price = 300 }
            );

            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, Name = "John Doe", Email = "johndoe@example.com" },
                new User { UserId = 2, Name = "Jane Smith", Email = "janesmith@example.com" }
            );

            modelBuilder.Entity<Booking>().HasData(
                new Booking { BookingId = 1, UserId = 1, BookingDate = DateTime.Parse("2024-12-14") }
            );

            modelBuilder.Entity<BookingPerformance>().HasData(
                new BookingPerformance { BookingId = 1, PerformanceId = 1 }
            );

        }
    }
}
