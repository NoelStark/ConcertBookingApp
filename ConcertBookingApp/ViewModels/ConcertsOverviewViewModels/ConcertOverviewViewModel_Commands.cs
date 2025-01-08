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

        /// <summary>
        ///  Method that directs the GUI to a different page
        ///  when pressing a concert
        /// </summary>
        /// <param name="concert">The clicked concert</param>
        /// <returns></returns>
        [RelayCommand]
        private async Task InspectConcert(Concert concert)
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

        /// <summary>
        /// Whenever a categorical filter is pressed, this method is called
        /// </summary>
        /// <param name="value">The category pressed</param>
        [RelayCommand]
        private void SelectedFilter(Category value)
        {
            //Changes color and filters the concerts

            Category? category = Categories.FirstOrDefault(x => x.Title == value.Title);
            if (category == null) return;

            category.IsSelected = !category.IsSelected;
            OnPropertyChanged(nameof(category.IsSelected));
            _selectedCategories = Categories.Where(x => x.IsSelected == true).ToList();
            FilterConcerts();
        }

        /// <summary>
        /// If the heart of a concert is pressed, the color of the heart changes
        /// </summary>
        /// <param name="value"></param>
        [RelayCommand]
        private void MakeFavorite(Concert value)
        {
            //Changes the color of the heart
            Concert? concert = Concerts.FirstOrDefault(x => x.Name == value.Name);
            if (concert == null) return;
            concert.IsFavorite = !concert.IsFavorite;
            OnPropertyChanged(nameof(concert.IsFavorite));

            //Not Implemented
        }

        /// <summary>
        /// When a range of dates is selected, it filter based on dates
        /// </summary>
        /// <param name="selectedRange">A range of dates, a start and end date</param>
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
