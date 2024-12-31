using Microsoft.EntityFrameworkCore;
using SharedResources.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Resources.Database
{
    public class DatabaseConnection : DbContext
    {

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingPerformance> BookingPerformances { get; set; }
        public DbSet<Performance> Performances { get; set; }
        public DbSet<Concert> Concerts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }

        public DatabaseConnection(DbContextOptions<DatabaseConnection> options) : base(options) 
        { 
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Concert>().HasMany(a => a.Performances).WithOne(b => b.Concert).HasForeignKey(b => b.ConcertId);

            modelBuilder.Entity<Booking>().HasMany(a => a.BookingPerformances).WithOne(b => b.Booking).HasForeignKey(b => b.BookingId);
            modelBuilder.Entity<Performance>().HasOne(a => a.Concert).WithMany(b => b.Performances).HasForeignKey(a => a.ConcertId);

            modelBuilder.Entity<Performance>().HasOne(a => a.BookingPerformance).WithOne(b => b.Performance).HasForeignKey<BookingPerformance>(b => b.PerformanceId);

            modelBuilder.Entity<User>().HasIndex(v => v.UserId).IsUnique();

            modelBuilder.Entity<Booking>().HasOne<User>().WithMany().HasForeignKey(b => b.UserId);
        }
        //static void Main(string[] args)
        //{
        //    using (DatabaseConnection context = new DatabaseConnection())
        //    {
        //        context.Database.EnsureCreated();
        //    }
        //}
    }
}
