using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConcertBookingApp.Data.Models;

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
        }
    }
}
