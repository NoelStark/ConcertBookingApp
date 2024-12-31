using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SharedResources.DTOs;
using SharedResources.Models;
using Syncfusion.Maui.Calendar;

namespace ConcertBookingApp.ViewModels.ConcertsOverviewViewModels
{
    public partial class ConcertOverviewViewModel
    {

        [RelayCommand]
        private async Task InspectConcert(ConcertDTO concert)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };
            string serializedConcert = JsonSerializer.Serialize(concert, options);
            string encodedConcert = Uri.EscapeDataString(serializedConcert);
            await Shell.Current.GoToAsync($"///ConcertDetailsPage?concert={encodedConcert}");
        }

        [RelayCommand]
        private void SelectedFilter(Category value)
        {
            Category? category = Categories.FirstOrDefault(x => x.Title == value.Title);
            if (category == null) return;

            category.IsSelected = !category.IsSelected;
            OnPropertyChanged(nameof(category.IsSelected));
            _selectedCategories = Categories.Where(x => x.IsSelected == true).ToList();
            FilterConcerts();
        }

        [RelayCommand]
        private void MakeFavorite(ConcertDTO value)
        {
            //Changes the color of the heart
            //Concert? concert = concerts.FirstOrDefault(x => x.Name == value.Name);
            //if (concert == null) return;
            //concert.IsFavorite = !concert.IsFavorite;
            //OnPropertyChanged(nameof(concert.IsFavorite));

            //Not Implemented
        }
        [RelayCommand]
        void SelectionChanged(CalendarDateRange selectedRange)
        {
            if (selectedRange is not { StartDate: not null, EndDate: not null }) return;

            DateTime? start = selectedRange.StartDate;
            DateTime? end = selectedRange.EndDate;

            if (!start.HasValue || !end.HasValue) return;

            startDate = (DateTime)start;
            endDate = (DateTime)end;
            FilterConcerts();
        }

        [RelayCommand]
        void ToggleCalender()
        {
            IsVisible = !IsVisible;
        }

        [RelayCommand]
        void ClearFilters()
        {
            RangeSelected = null;
            startDate = null;
            endDate = null;
            Categories.ForEach(c => c.IsSelected = false);
            filteredConcerts = _concertDTOs;
            UpdateConcerts(_concertDTOs);
        }
    }
}
