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
        private readonly BookingService _bookingService;
        private readonly ConcertService _concertService;
        public ObservableCollection<BookingPerformance> Performances { get; private set; }= new ObservableCollection<BookingPerformance>();
        
        public BookingViewModel(BookingService service, ConcertService concertService)
        {
            _bookingService = service;
            _concertService = concertService;
            _= AddPerformances();
        }

        public async Task AddPerformances()
        {
            //var hasPayed = Preferences.Get("HasPayed", false);
            //if (!hasPayed) return;

            List<Booking> bookings = await _bookingService.GetAllBookings(1);

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
                }
            }
        }

        [RelayCommand]
        public async void CancelBooking(BookingPerformance performance)
        {
            Performances.Remove(performance);
            OnPropertyChanged(nameof(Performances));
            await _bookingService.CancelBooking(performance.PerformanceId, performance.BookingId);
            
        }
    }
}
