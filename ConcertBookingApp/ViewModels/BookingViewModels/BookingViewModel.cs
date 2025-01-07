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
    public partial class BookingViewModel : ObservableObject
    {
        [ObservableProperty] private double opacity = 0.6;
        [ObservableProperty] private string searchInput = string.Empty;
        [ObservableProperty] private string subHeader = string.Empty;
        private readonly BookingService _bookingService;
        private readonly ConcertService _concertService;
        private readonly UserService _userService;
        private List<BookingPerformance> _performances = new List<BookingPerformance>();
        public ObservableCollection<BookingPerformance> Performances { get; private set; }= new ObservableCollection<BookingPerformance>();
        
        public BookingViewModel(BookingService service, ConcertService concertService, UserService userService)
        {
            _bookingService = service;
            _concertService = concertService;
            _= AddPerformances();
            _userService = userService;
        }

        public async Task AddPerformances()
        {
            Performances.Clear();

            List<Booking> bookings = await _bookingService.GetAllBookings(_userService.CurrentUser.UserId);

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

            SubHeader = Performances.Any() ? "See your booked events here" : "You have no Bookings";
        }

        private async Task Filter(string? oldValue, string newValue)
        {
            List<BookingPerformance> filteredConcerts = new List<BookingPerformance>();
            await Task.Run(() =>
            {
                if (newValue.Length < oldValue.Length)
                {
                    foreach (var performance in _performances)
                    {
                        Performances.Add(performance);
                    }
                }
                if (!string.IsNullOrEmpty(newValue))
                {
                    filteredConcerts = Performances.Where(x => x.Title.ToLower().Contains(newValue.ToLower())).ToList();
                    Performances.Clear();
                    foreach (var performance in filteredConcerts)
                    {
                        Performances.Add(performance);
                    }
                }
                else
                {
                    Performances.Clear();
                    foreach (var performance in _performances)
                    {
                        Performances.Add(performance);
                    }

                    return;
                }
            });
        }
        partial void OnSearchInputChanged(string? oldValue, string newValue)
        {
            _= Filter(oldValue, newValue);
        }

          

        [RelayCommand]
        public async void CancelBooking(BookingPerformance performance)
        {
            Performances.Remove(performance);
            OnPropertyChanged(nameof(Performances));
            await _bookingService.CancelBooking(performance.PerformanceId, performance.BookingId);
            SubHeader = Performances.Any() ? "See your booked events here" : "You have no Bookings";
        }
    }
}
