using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedResources.Models;

namespace SharedResources.Data
{
    
  
    public class ConcertRepository
    {
        private List<Concert> AllConcerts = new List<Concert>()
        {
            new Concert
            {
                ConcertId = 1, Description = "A high-energy event celebrating chart-topping hits and electrifying performances by popular pop artists.",
                Genre = "Pop", ImageUrl = "edm.png", Name = "Pop Pulse Festival"
            },
            new Concert
            {
                ConcertId = 2, Description = "A vibrant concert featuring a mix of iconic pop hits and fresh, emerging talent under dazzling lights.",
                Genre = "Jazz", ImageUrl = "testconcert.png", Name = "Starlight Pop Jazz"
            },
            new Concert
            {
                ConcertId = 3, Description = "A vibrant concert featuring a mix of iconic pop hits and fresh, emerging talent under dazzling lights.",
                Genre = "Classical", ImageUrl = "edm.png", Name = "Classical"
            }
        };

        public ConcertRepository()
        {
            AllConcerts[0].Performances = new List<Performance>()
            {
                new Performance
                {
                    TotalSeats = 5, AvailableSeats = 5, ConcertId = 1, Date = DateTime.Parse("2024-12-14"), Location = "Aspvägen",
                    Price = 100, PerformanceId = 1
                },
                new Performance
                {
                    TotalSeats = 150, AvailableSeats = 150, ConcertId = 1, Date = DateTime.Now, Location = "Aspvägen",
                    Price = 200, PerformanceId = 2
                },
                new Performance
                {
                    TotalSeats = 200, AvailableSeats = 200, ConcertId = 1, Date = DateTime.Now, Location = "Aspvägen",
                    Price = 300, PerformanceId = 3
                }
            };

            AllConcerts[1].Performances = new List<Performance>()
            {
                new Performance
                {
                    TotalSeats = 5, AvailableSeats = 5, ConcertId = 2, Date = DateTime.Parse("2024-10-12"), Location = "Aspvägen",
                    Price = 100, PerformanceId = 4
                },
                new Performance
                {
                    TotalSeats = 150, AvailableSeats = 150, ConcertId = 2, Date = DateTime.Parse("2024-07-04"), Location = "Aspvägen",
                    Price = 200, PerformanceId = 6
                },
                new Performance
                {
                    TotalSeats = 200, AvailableSeats = 200, ConcertId = 2, Date = DateTime.Parse("2024-10-13"), Location = "Aspvägen",
                    Price = 300, PerformanceId = 7
                }
            };

            AllConcerts[2].Performances = new List<Performance>()
            {
                new Performance
                {
                    TotalSeats = 5, AvailableSeats = 5, ConcertId = 3, Date = DateTime.Parse("2024-01-02"), Location = "Aspvägen",
                    Price = 100, PerformanceId = 8
                },
                new Performance
                {
                    TotalSeats = 150, AvailableSeats = 150, ConcertId = 3, Date = DateTime.Now, Location = "Aspvägen",
                    Price = 200, PerformanceId = 9
                },
                new Performance
                {
                    TotalSeats = 200, AvailableSeats = 200, ConcertId = 3, Date = DateTime.Now, Location = "Aspvägen",
                    Price = 300, PerformanceId = 10
                }
            };
        }
        //Klar
        public List<Concert> GetAllConcerts()
        {
            return AllConcerts;
        }

        //Klar
        public Concert GetConcertForPerformance(int performanceId)
        {
            Concert? concert = AllConcerts.FirstOrDefault(x => x.Performances.Any(y => y.PerformanceId == performanceId));
            if (concert == null) return new Concert();
            return concert;
        }

        //Klar
        public List<Performance> GetPerformances(int concertId)
        {
            List<Performance> performances = AllConcerts.FirstOrDefault(x => x.ConcertId == concertId ).Performances;
            return performances;
        }
    }
}
