using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using SharedResources.Models;
using ConcertBookingApp.Services;
using SharedResources.DTOs;
using ConcertBookingApp.Data.Database;
using ConcertBookingApp.Views;


namespace ConcertBookingApp.ViewModels.CheckoutViewModels
{
    public partial class CheckoutViewModel
    {
        [RelayCommand]
        private void IncreaseQuantity(BookingPerformance performance)
        {
            BookingPerformance chosenPerformance = _bookingService.CurrentBooking.BookingPerformances.FirstOrDefault(b => b.Performance.PerformanceId == performance.Performance.PerformanceId);

            if(chosenPerformance.Performance.AvailableSeats != 0)
            {
                chosenPerformance.SeatsBooked++;
                chosenPerformance.Performance.AvailableSeats--;
            }

            FillFlattenedPerformances();
            UpdatePrice();
        }

        [RelayCommand]
        private void DecreaseQuantity(BookingPerformance performanceDTO)
        {
            BookingPerformance chosenPerformance = _bookingService.CurrentBooking.BookingPerformances.FirstOrDefault(b => b.Performance.PerformanceId == performanceDTO.Performance.PerformanceId);

            chosenPerformance.SeatsBooked--;
            chosenPerformance.Performance.AvailableSeats++;
            if (chosenPerformance.SeatsBooked == 0)
            {
                Booking findBooking = _bookingService.CurrentBooking;
                findBooking.BookingPerformances.Remove(chosenPerformance);
                if (!findBooking.BookingPerformances.Any())
                    _bookingService.CurrentBooking = new Booking();
            }
            FillFlattenedPerformances();
            UpdatePrice();
        }


        [RelayCommand]
        private async Task GoBack()
        {
            await Shell.Current.GoToAsync("///ConcertOverviewPage");
        }

        [RelayCommand]
        private async Task Continue()
        {
            await Shell.Current.GoToAsync(nameof(PaymentPage));
        }
    }
}
