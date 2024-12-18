﻿using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConcertBookingApp.Models;
using System.Collections.ObjectModel;
using Syncfusion.Maui.Calendar;

namespace ConcertBookingApp.ViewModels.ConcertsOverviewViewModels
{
    public partial class ConcertOverviewViewModel
    {
        [ObservableProperty] private string searchInput = string.Empty;
        [ObservableProperty] private bool isVisible = false;
        [ObservableProperty] private int concertCount;
        [ObservableProperty] private CalendarDateRange? rangeSelected;
        private string _lastSearchInput = string.Empty;
        private List<Concert> _cachedConcerts = AllConcerts;
        private List<Category> _selectedCategories = new List<Category>();
        private DateTime? startDate = null;
        private DateTime? endDate = null;
        partial void OnSearchInputChanged(string value)
        {
            FilterConcerts(searchText:value);
        }
        public ObservableCollection<Concert> Concerts { get; set; } = new ObservableCollection<Concert>(AllConcerts);


        public List<Category> Categories { get; set; } = new List<Category>()
        {
            new Category { ImageSource = "violin.png", Title = "Classical" },
            new Category { ImageSource = "saxophone.png", Title = "Jazz" },
            new Category { ImageSource = "guitar.png", Title = "Pop" },
            new Category { ImageSource = "headphones.png", Title = "EDM" }
        };

        private static readonly List<Concert> AllConcerts = new List<Concert>()
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
                Genre = "Jazz", ImageUrl = "edm.png", Name = "Starlight Pop Jazz",
                Performances = new List<Performance>()
                {
                    new Performance{ AvailableSeats = 100, ConcertId = 2, Date = DateTime.Parse("2024-10-04")}
                }
            },
            new Concert
            {
                ConcertId = 3, Description = "A vibrant concert featuring a mix of iconic pop hits and fresh, emerging talent under dazzling lights.",
                Genre = "Classical", ImageUrl = "edm.png", Name = "Classical",
                Performances = new List<Performance>()
                {
                    new Performance{ AvailableSeats = 100, ConcertId = 3, Date = DateTime.Parse("2025-09-22")}
                }
            }
        };

     
    }
}
