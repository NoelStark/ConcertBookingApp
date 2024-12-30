using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using SharedResources.DTOs;
using SharedResources.Models;
using Syncfusion.Maui.Calendar;

namespace ConcertBookingApp.ViewModels.ConcertsOverviewViewModels
{
    public partial class ConcertOverviewViewModel
    {
        [ObservableProperty] private string searchInput = string.Empty;
        [ObservableProperty] private bool isVisible = false;
        [ObservableProperty] private int concertCount;
        [ObservableProperty] private CalendarDateRange? rangeSelected;
        //private string _lastSearchInput = string.Empty;
        //private List<Concert> _cachedConcerts = AllConcerts;
        private List<Category> _selectedCategories = new List<Category>();
        private DateTime? startDate = null;
        private DateTime? endDate = null;
        private List<ConcertDTO> concerts = new List<ConcertDTO>();

        partial void OnSearchInputChanged(string value)
        {
            FilterConcerts(searchText:value.ToLower());
        }
        public ObservableCollection<ConcertDTO> Concerts { get; set; }


        public List<Category> Categories { get; set; } = new List<Category>()
        {
            new Category { ImageSource = "violin.png", Title = "Classical" },
            new Category { ImageSource = "saxophone.png", Title = "Jazz" },
            new Category { ImageSource = "guitar.png", Title = "Pop" },
            new Category { ImageSource = "headphones.png", Title = "EDM" }
        };

      
    }
}
