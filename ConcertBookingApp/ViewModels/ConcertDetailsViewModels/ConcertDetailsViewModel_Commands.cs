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
        async Task GoBack() //Lets the user direct to the last page
        {
            await Shell.Current.GoToAsync($"///ConcertOverviewPage");
        }

        /// <summary>
        /// This method allows the user to increase the quantity of a perfornance.
        /// The user can increase the quantity unleass the avalible seats are greater than 0
        /// </summary>

        [RelayCommand]
        void IncreaseQuantity(BookingPerformance bookingPerformance)
        {
            string value = "Increase";
            if (bookingPerformance.Performance.AvailableSeats != 0)
            {
                bookingPerformance.SeatsBooked++;
                bookingPerformance.Performance.AvailableSeats--;
            }
            UpdateButton();
        }

        /// <summary>
        /// This method lets the user to decrease quantity of perfromances
        /// If the quantity is 0 it removes the performance from the current booking and its bookingperfromance
        /// </summary>

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

        /// <summary>
        /// Lets the user to buy tickets, its connected to the button. 
        /// </summary>
        /// <returns></returns>


        [RelayCommand]
        private async Task BuyTickets()
        {
            //Filter performances with booked seats from all performances for the concert.
            List<BookingPerformance> hasse = AllPerformancesForConcert.Where(x => x.SeatsBooked > 0).ToList();
            foreach (var performance in hasse)
            {
                //Update the performance details with concert information.
                performance.ImageURL = Concert.ImageUrl;
                performance.Title = Concert.Name;
                performance.PerformanceId = performance.Performance.PerformanceId;
            }
            if (bookingService.CurrentBooking != null)  //If there's an existing booking, update it with the new performances seatsbooked and also decrease the availible seats
            {
                Booking currentBooking = bookingService.CurrentBooking;
                foreach (var performance in hasse)
                {
                    BookingPerformance alreadyexist = bookingService.CurrentBooking.BookingPerformances.FirstOrDefault(a => a.Performance.PerformanceId == performance.Performance.PerformanceId);

                    if (alreadyexist != null)
                    {
                        alreadyexist.SeatsBooked += performance.SeatsBooked;
                        alreadyexist.Performance.AvailableSeats = alreadyexist.Performance.TotalSeats - alreadyexist.SeatsBooked;
                    }
                    else
                        currentBooking.BookingPerformances.Add(performance);
                }
            }
            else //Else it creates a new booking
            {
                bookingService.CurrentBooking = new Booking
                {
                    BookingPerformances = new List<BookingPerformance>(hasse),
                    UserId = _userService.CurrentUser.UserId
                };
            }

            //resets the button for the gui
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

