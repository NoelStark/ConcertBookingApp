using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ConcertBookingApp.Models;
using Syncfusion.Maui.Calendar;

namespace ConcertBookingApp.ViewModels.ConcertsOverviewViewModels
{
    public partial class ConcertOverviewViewModel : ObservableObject
    {

        [RelayCommand]
        private async Task InspectConcert(Concert concert)
        {
            string serializedConcert = JsonSerializer.Serialize(concert);
            string encodedConcert = Uri.EscapeDataString(serializedConcert);
            await Shell.Current.GoToAsync($"///ConcertDetailsPage?concert={encodedConcert}");
        }

        [RelayCommand]
        private async void SelectedFilter(Category value)
        {
            Category? category = Categories.FirstOrDefault(x => x.Title == value.Title);
            if (category == null) return;

            category.IsSelected = !category.IsSelected;
            OnPropertyChanged(nameof(category.IsSelected));
            bool isAnySelected = Categories.Any(x => x.IsSelected == true);
            if (isAnySelected)
            {
                List<Category> selectedCategories = Categories.Where(x => x.IsSelected == true).ToList();
                FilterConcerts(_cachedConcerts, selectedCategories: selectedCategories);
            }
            else
            {
                Concerts = new ObservableCollection<Concert>(AllConcerts);
            }
            OnPropertyChanged(nameof(Concerts));
        }

        [RelayCommand]
        private void MakeFavorite(Concert value)
        {
            Concert? concert = Concerts.FirstOrDefault(x => x.Name == value.Name);
            if (concert == null) return;
            concert.IsFavorite = !concert.IsFavorite;
            OnPropertyChanged(nameof(concert.IsFavorite));
        }
        [RelayCommand]
        async void SelectionChanged(CalendarDateRange selectedRange)
        {
            if (selectedRange.StartDate != null && selectedRange.EndDate != null)
            {
                var startDate = selectedRange.StartDate;
                var endDate = selectedRange.EndDate;
                FilterConcerts(_cachedConcerts, startDate: startDate, endDate: endDate);
            }
        }

        [RelayCommand]
        void ToggleCalender()
        {
            IsVisible = !IsVisible;
        }
    }
}
