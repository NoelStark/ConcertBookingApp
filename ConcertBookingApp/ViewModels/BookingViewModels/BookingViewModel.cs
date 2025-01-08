using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ConcertBookingApp.Services;
using SharedResources.DTOs;
using SharedResources.Models;

namespace ConcertBookingApp.ViewModels.BookingViewModels
{
    public partial class BookingViewModel
    {
        public BookingViewModel(BookingService service, ConcertService concertService, UserService userService)
        {
            _bookingService = service;
            _concertService = concertService;
            _ = AddPerformances();
            _userService = userService;
        }
        /// <summary>
        /// The method that adds all the bookings to the GUI
        /// </summary>
        /// <returns></returns>
        public async Task AddPerformances()
        {
            Performances.Clear();
            _performances.Clear();
            List<Booking> bookings = await _bookingService.GetAllBookings(_userService.CurrentUser.UserId);

            //Goes through every booking and the performances to fill the GUI with
            foreach (var booking in bookings)
            {
                booking.BookingPerformances = await _bookingService.GetPerformancesForBooking(booking.BookingId);

                foreach (var performance in booking.BookingPerformances)
                {
                    Performance findPerformance = await _concertService.GetPerformance(performance.PerformanceId);
                    Concert concert = await _concertService.GetConcertForPerformance(performance.PerformanceId);
                    performance.Title = concert.Name;
                    performance.ImageURL = concert.ImageUrl;
                    performance.Performance = findPerformance;
                    Performances.Add(performance);
                    _performances.Add(performance);
                }
            }

            //Alters the text based on if the user has booked tickets or not
            SubHeader = Performances.Any() ? "See your booked events here" : "You have no Bookings";
        }

        /// <summary>
        /// Filtering for the searchbar that checks new vs old values to fill the gui with appropriate bookings
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        private async Task Filter(string? oldValue, string newValue)
        {
            List<BookingPerformance> filteredConcerts = new List<BookingPerformance>();
            await Task.Run(() =>
            {
                //If the user deletes a letter
                if (newValue.Length < oldValue.Length)
                {
                    Performances.Clear();
                    foreach (var performance in _performances)
                    {
                        Performances.Add(performance);
                    }
                }

                //Assuming the searchbar has information, filtering happens based on title
                if (!string.IsNullOrEmpty(newValue))
                {
                    Performances.Clear();
                    filteredConcerts = _performances.Where(x => x.Title.ToLower().Contains(newValue.ToLower())).ToList();
                    foreach (var performance in filteredConcerts)
                    {
                        Performances.Add(performance);
                    }
                }
                else
                {
                    //If the searchbar is empty, reset the performances
                    Performances.Clear();
                    foreach (var performance in _performances)
                    {
                        Performances.Add(performance);
                    }

                    return;
                }
            });
        }
    }
}
