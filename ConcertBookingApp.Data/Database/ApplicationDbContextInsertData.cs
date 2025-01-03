using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConcertBookingApp.Data.Models;

namespace ConcertBookingApp.Data.Database
{
    public static class ApplicationDbContextInsertData
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (!context.Concerts.Any())
            {
                context.Concerts.AddRange(
                    new Concert { ConcertId = 1, Description = "Pop Pulse Festival", Genre = "Pop", ImageUrl = "edm.png", Name = "Pop Pulse Festival" },
                    new Concert { ConcertId = 2, Description = "Starlight Pop Jazz", Genre = "Jazz", ImageUrl = "testconcert.png", Name = "Starlight Pop Jazz" },
                    new Concert { ConcertId = 3, Description = "Classical", Genre = "Classical", ImageUrl = "edm.png", Name = "Classical" }
                );
            }

            if (!context.Performances.Any())
            {
                context.Performances.AddRange(
                    new Performance { PerformanceId = 1, ConcertId = 1, TotalSeats = 5, Date = DateTime.Parse("2024-12-14"), Location = "Aspvägen", Price = 100 },
                    new Performance { PerformanceId = 2, ConcertId = 1, TotalSeats = 150, Date = DateTime.Now, Location = "Aspvägen", Price = 200 },
                    new Performance { PerformanceId = 3, ConcertId = 1, TotalSeats = 200, Date = DateTime.Now, Location = "Aspvägen", Price = 300 },
                    new Performance { PerformanceId = 4, ConcertId = 2, TotalSeats = 5, Date = DateTime.Parse("2024-10-12"), Location = "Aspvägen", Price = 100 },
                    new Performance { PerformanceId = 6, ConcertId = 2, TotalSeats = 150, Date = DateTime.Parse("2024-07-04"), Location = "Aspvägen", Price = 200 },
                    new Performance { PerformanceId = 7, ConcertId = 2, TotalSeats = 200, Date = DateTime.Parse("2024-10-13"), Location = "Aspvägen", Price = 300 },
                    new Performance { PerformanceId = 8, ConcertId = 3, TotalSeats = 5, Date = DateTime.Parse("2024-01-02"), Location = "Aspvägen", Price = 100 },
                    new Performance { PerformanceId = 9, ConcertId = 3, TotalSeats = 150, Date = DateTime.Now, Location = "Aspvägen", Price = 200 },
                    new Performance { PerformanceId = 10, ConcertId = 3, TotalSeats = 200, Date = DateTime.Now, Location = "Aspvägen", Price = 300 }
                );
            }

            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User { UserId = 1, Name = "John Doe", Email = "johndoe@example.com" },
                    new User { UserId = 2, Name = "Jane Smith", Email = "janesmith@example.com" }
                );
            }

            if (!context.Bookings.Any())
            {
                context.Bookings.Add(
                    new Booking { BookingId = 1, UserId = 1, BookingDate = DateTime.Now }
                );
            }

            if (!context.BookingPerformances.Any())
            {
                context.BookingPerformances.Add(
                    new BookingPerformance { BookingId = 1, PerformanceId = 1 }
                );
            }

            context.SaveChanges();
        }
    }
}
