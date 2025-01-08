using CommunityToolkit.Mvvm.Input;
using SharedResources.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBookingApp.ViewModels.BookingViewModels
{
    public partial class BookingViewModel
    {
        /// <summary>
        /// When the cancel button of the booking is pressed, it removes from UI and database
        /// </summary>
        /// <param name="performance"></param>

        [RelayCommand]
        public async void CancelBooking(BookingPerformance performance)
        {
            Performances.Remove(performance);
            OnPropertyChanged(nameof(Performances));
            await _bookingService.CancelBooking(performance.PerformanceId, performance.BookingId);
            SubHeader = Performances.Any() ? "See your booked events here" : "You have no Bookings";
        }

        partial void OnSearchInputChanged(string? oldValue, string newValue)
        {
            _ = Filter(oldValue, newValue);
        }
    }
}
