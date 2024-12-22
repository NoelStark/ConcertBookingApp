using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ConcertBookingApp.Models;

namespace ConcertBookingApp.ViewModels.ConcertsOverviewViewModels
{
    public partial class ConcertOverviewViewModel : ObservableObject
    {
        public ConcertOverviewViewModel()
        {
            AllConcerts[0].Performances = new List<Performance>()
            {
                new Performance{ TotalSeats = 5, AvailableSeats = 5, ConcertId = 1, Date = DateTime.Now, Location = "Aspvägen", Price = 100, PerformanceId = 1, Concert = AllConcerts[0]},
                new Performance{ TotalSeats = 150, AvailableSeats = 150, ConcertId = 1, Date = DateTime.Now, Location = "Aspvägen", Price = 200, PerformanceId = 2, Concert = AllConcerts[0]},
                new Performance{ TotalSeats = 200, AvailableSeats = 200, ConcertId = 1, Date = DateTime.Now, Location = "Aspvägen", Price = 300, PerformanceId = 3, Concert = AllConcerts[0]}
            };

            AllConcerts[1].Performances = new List<Performance>()
            {
                new Performance{ TotalSeats = 5, AvailableSeats = 5, ConcertId = 1, Date = DateTime.Now, Location = "Aspvägen", Price = 100, PerformanceId = 1, Concert = AllConcerts[0]},
                new Performance{ TotalSeats = 150, AvailableSeats = 150, ConcertId = 1, Date = DateTime.Now, Location = "Aspvägen", Price = 200, PerformanceId = 2, Concert = AllConcerts[0]},
                new Performance{ TotalSeats = 200, AvailableSeats = 200, ConcertId = 1, Date = DateTime.Now, Location = "Aspvägen", Price = 300, PerformanceId = 3, Concert = AllConcerts[0]}
            };

            AllConcerts[2].Performances = new List<Performance>()
            {
                new Performance{ TotalSeats = 5, AvailableSeats = 5, ConcertId = 1, Date = DateTime.Now, Location = "Aspvägen", Price = 100, PerformanceId = 1, Concert = AllConcerts[0]},
                new Performance{ TotalSeats = 150, AvailableSeats = 150, ConcertId = 1, Date = DateTime.Now, Location = "Aspvägen", Price = 200, PerformanceId = 2, Concert = AllConcerts[0]},
                new Performance{ TotalSeats = 200, AvailableSeats = 200, ConcertId = 1, Date = DateTime.Now, Location = "Aspvägen", Price = 300, PerformanceId = 3, Concert = AllConcerts[0]}
            };
            UpdateConcerts(AllConcerts);
        }

        private void UpdateConcerts(List<Concert> concerts)
        {
            Concerts.Clear();
            foreach (var concert in concerts)
            {
                Concerts.Add(concert);
            }

            ConcertCount = Concerts.Count;
            OnPropertyChanged(nameof(Concerts));
            OnPropertyChanged(nameof(ConcertCount));
        }

        public List<Concert> filteredConcerts = new List<Concert>(AllConcerts);
        private void FilterConcerts(string? searchText = null, List<Concert>? concerts = null)
        {
            concerts ??= AllConcerts;

            if (!string.IsNullOrEmpty(searchText))
            {
                concerts = concerts.Where(c =>
                    c.Genre.ToLower().Contains(searchText.ToLower()) ||
                    c.Name.ToLower().Contains(searchText.ToLower())).ToList();
            }

            if (startDate != null && endDate != null)
            {
                concerts = concerts.Where(c =>
                    c.Performances.Any(p => p.Date > startDate && p.Date < endDate)).ToList();
            }

            if (_selectedCategories.Any())
            {
                var selectedCategories = Categories.Where(x => x.IsSelected).ToList();
                concerts = concerts.Where(c =>
                    selectedCategories.Any(category => category.Title == c.Genre)).ToList();
            }

            filteredConcerts = concerts;
            UpdateConcerts(filteredConcerts);
        }




    }
}
