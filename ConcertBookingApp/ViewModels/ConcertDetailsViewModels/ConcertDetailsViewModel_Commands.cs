using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConcertBookingApp.Views;
using SharedResources.Models;
using CommunityToolkit.Mvvm.Input;
//using ConcertBookingApp.Data.Models;

namespace ConcertBookingApp.ViewModels.ConcertDetailsViewModels
{
    public partial class ConcertDetailsViewModel
    {
        [RelayCommand]
        async Task GoBack()
        {
            await Shell.Current.GoToAsync($"///ConcertOverviewPage");
        }
        [RelayCommand]
        void IncreaseQuantity(BookingPerformance bookingPerformance)
        {
            string value = "Increase";
            bookingPerformance.SeatsBooked++;
            bookingPerformance.Performance.AvailableSeats--;
            if (bookingPerformance.Performance.AvailableSeats == 0)
                AddTicketsVisible = false;
            UpdateButton();
        }

        [RelayCommand]
        void DecreaseQuantity(BookingPerformance bookingPerformance)
        {
            if (bookingPerformance.SeatsBooked > 0)
            {
                bookingPerformance.SeatsBooked--;
                bookingPerformance.Performance.AvailableSeats++;
                AddTicketsVisible = true;
                UpdateButton();
            }
        }

        [RelayCommand]
        private async Task BuyTickets()
        {
            List<BookingPerformance> hasse = AllPerformancesForConcert.Where(x => x.SeatsBooked > 0).ToList();
            foreach (var performance in hasse)
            {
                performance.ImageURL = Concert.ImageUrl;
                performance.Title = Concert.Name;
            }
            if (bookingService.CurrentBooking != null)
            {
                Booking currentBooking = bookingService.CurrentBooking;
                foreach (var performance in hasse)
                {
                    BookingPerformance alreadyexist = bookingService.CurrentBooking.BookingPerformances.FirstOrDefault(a => a.Performance.PerformanceId == performance.Performance.PerformanceId);
                    currentBooking.BookingPerformances.Add(performance);
                    if (alreadyexist != null)
                    {
                        performance.SeatsBooked += alreadyexist.SeatsBooked;
                        performance.Performance.AvailableSeats = performance.Performance.TotalSeats - performance.SeatsBooked;
                        currentBooking.BookingPerformances.Remove(alreadyexist);
                    }
                    
                }
            }
            else
            {
                bookingService.CurrentBooking = new Booking
                {
                    BookingPerformances = new List<BookingPerformance>(hasse)
                };
            }

            AddedToCart = true;
            _ = ResetCartButton();
        }

        [RelayCommand]
        private void MakeFavorite()
        {
            //Changes the color of the heart
            if (concert == null) return;
            concert.IsFavorite = !concert.IsFavorite;
            OnPropertyChanged(nameof(concert.IsFavorite));

            //Not Implemented
        }
    }
}

