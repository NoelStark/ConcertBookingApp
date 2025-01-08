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
        /// <summary>
        /// This method allows the user to increase the quantity of a perfornance.
        /// The user can increase the quantity unleass the avalible seats are greater than 0
        /// </summary>
        
        [RelayCommand]
        private void IncreaseQuantity(BookingPerformance performance)
        {
            BookingPerformance chosenPerformance = _bookingService.CurrentBooking.BookingPerformances.FirstOrDefault(b => b.Performance.PerformanceId == performance.Performance.PerformanceId);

            if(chosenPerformance.Performance.AvailableSeats != 0)
            {
                chosenPerformance.SeatsBooked++;
                chosenPerformance.Performance.AvailableSeats--;
            }

            FillFlattenedPerformances(); //Updates the gui with correct quantity for each perfromance
            UpdatePrice();
        }

        /// <summary>
        /// This method lets the user to decrease quantity of perfromances
        /// If the quantity is 0 it removes the performance from the current booking and its bookingperfromance
        /// if the cart have no bookingperformances it will remove the old booking and reset it by adding a new clean booking
        /// </summary>

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
        private async Task GoBack() //Lets user direct to the last page
        {
            await Shell.Current.GoToAsync("///ConcertOverviewPage");
        }

        [RelayCommand]
        private async Task Continue() //Lets user direct to the next page
        {
            await Shell.Current.GoToAsync($"///PaymentPage?totalPrice={TotalPrice}");
        }
    }
}
