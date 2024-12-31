﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
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
            //Bookings.Booking.BookingPerformance
            _= Test();
        }

        private async Task Test()
        {
            var list = new List<BookingPerformance>();
            foreach (var performance in _bookingService.Bookings.SelectMany(booking => booking.BookingPerformances))
            {
                ConcertDTO concert = await _concertService.GetConcertForPerformance(performance.Performance.PerformanceId);
                performance.Title = concert.Name;
                performance.ImageURL = concert.ImageUrl;
                Performances.Add(performance);
            }
        }
    }
}
