﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConcertBookingApp.Models;

namespace ConcertBookingApp.Data
{
    
  
    public class ConcertRepository
    {
        private List<Concert> AllConcerts = new List<Concert>()
        {
            new Concert
            {
                ConcertId = 1, Description = "A high-energy event celebrating chart-topping hits and electrifying performances by popular pop artists.",
                Genre = "Pop", ImageUrl = "edm.png", Name = "Pop Pulse Festival",
                Performances = new List<Performance>()
                {
                    new Performance{ TotalSeats = 5, AvailableSeats = 5, ConcertId = 1, Date = DateTime.Now, Location = "Aspvägen", Price = 100, PerformanceId = 1},
                    new Performance{ TotalSeats = 150, AvailableSeats = 150, ConcertId = 1, Date = DateTime.Now, Location = "Aspvägen", Price = 200, PerformanceId = 2},
                    new Performance{ TotalSeats = 200, AvailableSeats = 200, ConcertId = 1, Date = DateTime.Now, Location = "Aspvägen", Price = 300, PerformanceId = 3}
                }
            },
            new Concert
            {
                ConcertId = 2, Description = "A vibrant concert featuring a mix of iconic pop hits and fresh, emerging talent under dazzling lights.",
                Genre = "Jazz", ImageUrl = "testconcert.png", Name = "Starlight Pop Jazz",
                Performances = new List<Performance>()
                {
                    new Performance{TotalSeats = 150, AvailableSeats = 100, ConcertId = 2, Date = DateTime.Parse("2024-10-04"), Location = "Aspvägen", Price = 100, PerformanceId = 4}
                }
            },
            new Concert
            {
                ConcertId = 3, Description = "A vibrant concert featuring a mix of iconic pop hits and fresh, emerging talent under dazzling lights.",
                Genre = "Classical", ImageUrl = "edm.png", Name = "Classical",
                Performances = new List<Performance>()
                {
                    new Performance{TotalSeats = 150, AvailableSeats = 100, ConcertId = 3, Date = DateTime.Parse("2025-09-22"), Location = "Aspvägen", Price = 100, PerformanceId = 5}
                }
            }
        };

        public ConcertRepository()
        {
            AllConcerts[0].Performances = new List<Performance>()
            {
                new Performance
                {
                    TotalSeats = 5, AvailableSeats = 5, ConcertId = 1, Date = DateTime.Parse("2024-12-14"), Location = "Aspvägen",
                    Price = 100, PerformanceId = 1, Concert = AllConcerts[0]
                },
                new Performance
                {
                    TotalSeats = 150, AvailableSeats = 150, ConcertId = 1, Date = DateTime.Now, Location = "Aspvägen",
                    Price = 200, PerformanceId = 2, Concert = AllConcerts[0]
                },
                new Performance
                {
                    TotalSeats = 200, AvailableSeats = 200, ConcertId = 1, Date = DateTime.Now, Location = "Aspvägen",
                    Price = 300, PerformanceId = 3, Concert = AllConcerts[0]
                }
            };

            AllConcerts[1].Performances = new List<Performance>()
            {
                new Performance
                {
                    TotalSeats = 5, AvailableSeats = 5, ConcertId = 1, Date = DateTime.Parse("2024-10-12"), Location = "Aspvägen",
                    Price = 100, PerformanceId = 1, Concert = AllConcerts[0]
                },
                new Performance
                {
                    TotalSeats = 150, AvailableSeats = 150, ConcertId = 1, Date = DateTime.Parse("2024-07-04"), Location = "Aspvägen",
                    Price = 200, PerformanceId = 2, Concert = AllConcerts[0]
                },
                new Performance
                {
                    TotalSeats = 200, AvailableSeats = 200, ConcertId = 1, Date = DateTime.Parse("2024-10-13"), Location = "Aspvägen",
                    Price = 300, PerformanceId = 3, Concert = AllConcerts[0]
                }
            };

            AllConcerts[2].Performances = new List<Performance>()
            {
                new Performance
                {
                    TotalSeats = 5, AvailableSeats = 5, ConcertId = 1, Date = DateTime.Now, Location = "Aspvägen",
                    Price = 100, PerformanceId = 1, Concert = AllConcerts[0]
                },
                new Performance
                {
                    TotalSeats = 150, AvailableSeats = 150, ConcertId = 1, Date = DateTime.Now, Location = "Aspvägen",
                    Price = 200, PerformanceId = 2, Concert = AllConcerts[0]
                },
                new Performance
                {
                    TotalSeats = 200, AvailableSeats = 200, ConcertId = 1, Date = DateTime.Now, Location = "Aspvägen",
                    Price = 300, PerformanceId = 3, Concert = AllConcerts[0]
                }
            };
        }
        
        public List<Concert> GetAllConcerts()
        {
            return AllConcerts;
        }

        public List<Performance> GetPerformances(int concertId)
        {
            List<Performance> performances = AllConcerts.FirstOrDefault(x => x.ConcertId == concertId ).Performances;
            return performances;
        }
    }
}
