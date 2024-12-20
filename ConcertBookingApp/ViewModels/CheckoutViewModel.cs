using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ConcertBookingApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ConcertBookingApp.Services;
using ConcertBookingApp.ViewModels.ConcertsOverviewViewModels;

namespace ConcertBookingApp.ViewModels
{
    public partial class CheckoutViewModel : ObservableObject
    {
        [ObservableProperty]
        private double totalPrice = 0;

        [ObservableProperty]
        private int totalAmountOfItems = 0;

        [ObservableProperty]
        private bool canBeClicked = false;

        [ObservableProperty]
        private bool addTicketsVisible = true;

        [ObservableProperty]
        private Concert concert;

        [ObservableProperty]
        private Performance performance;

        [ObservableProperty]
        private Booking booking;

        [ObservableProperty]
        private BookingPerformance bookingPerformance;

        private readonly BookingService _bookingService;

        private readonly ConcertOverviewViewModel _concertViewModel; 
        public ObservableCollection<List<Booking>> BookingsCart { get; set; } = new ObservableCollection<List<Booking>>();
        public ObservableCollection<Booking> AllBookings { get; set; } = new ObservableCollection<Booking>();
        public ObservableCollection<Concert> SelectedConcerts { get; set; } = new ObservableCollection<Concert>();
        public ObservableCollection<Performance> AllPerformances { get; set; } = new ObservableCollection<Performance>();
        public CheckoutViewModel(BookingService bookingService)
        {
            _bookingService = bookingService;
            _concertViewModel = new ConcertOverviewViewModel();
            LoadBookings();
            UpdatePrice();
        }

        private void LoadBookings()
        {
            BookingsCart.Clear();
            AllBookings.Clear();
            AllPerformances.Clear();
            SelectedConcerts.Clear();

            BookingsCart.Add(_bookingService.Bookings);
            if (BookingsCart.Any())
                CanBeClicked = true;

            foreach (var item in _bookingService.Bookings)
                AllBookings.Add(item);

            List<Performance> findPerformances = AllBookings.SelectMany(a => a.BookingPerformances).Select(a => a.Performance).ToList();
            List<BookingPerformance> findBookingPerformances = AllBookings.SelectMany(a => a.BookingPerformances).ToList();
            List<Concert> allConcerts = _concertViewModel.Concerts.ToList();
            List<Concert> matchingConcerts = allConcerts.Where(a => findPerformances.Any(b => b.ConcertId == a.ConcertId)).ToList();
            foreach (Concert concert in matchingConcerts)
                SelectedConcerts.Add(concert);
            foreach (Performance performance in findPerformances)
            {
                Performance performanceFound = SelectedConcerts.SelectMany(c => c.Performances).FirstOrDefault(p => p.PerformanceId == performance.PerformanceId);
                if (performanceFound != null)
                    AllPerformances.Add(performanceFound);
            }

            foreach (var concert in SelectedConcerts)
                foreach (var performance in concert.Performances)
                    performance.Concert = concert;

            foreach (var bookingList in BookingsCart)
                foreach (var booking in bookingList)
                    foreach (var bookingPerformance in booking.BookingPerformances)
                        foreach (var concert in SelectedConcerts)
                        {
                            Performance foundPerfromance = concert.Performances.FirstOrDefault(a => a.PerformanceId == bookingPerformance.Performance.PerformanceId);
                            foundPerfromance.BookingPerformance = bookingPerformance;
                        }
        }

        private async Task UpdatePrice()
        {
            TotalAmountOfItems = BookingsCart.SelectMany(a => a).SelectMany(b => b.BookingPerformances).Sum(c => c.SeatsBooked);
            TotalPrice = BookingsCart.SelectMany(a => a).SelectMany(b => b.BookingPerformances).Sum(c => c.SeatsBooked * c.Performance.Price);
        }

        [RelayCommand]
        void IncreaseQuantity(Performance performance)
        {
            BookingPerformance findPerformances = AllBookings.SelectMany(a => a.BookingPerformances).FirstOrDefault(b => b.Performance.PerformanceId == performance.PerformanceId);
            findPerformances.SeatsBooked++;
            findPerformances.Performance.AvailableSeats--;
            if (findPerformances.Performance.AvailableSeats == 0)
                AddTicketsVisible = false;
            _ = UpdatePrice();
        }

        [RelayCommand]
        void DecreaseQuantity(Performance performance)
        {
            BookingPerformance findPerformances = AllBookings.SelectMany(a => a.BookingPerformances).FirstOrDefault(b => b.Performance.PerformanceId == performance.PerformanceId);
            findPerformances.SeatsBooked--;
            findPerformances.Performance.AvailableSeats++;
            if (findPerformances.SeatsBooked == 0)
            {
                Booking findBooking = _bookingService.Bookings.FirstOrDefault(a => a.Performances.Any(b => b.PerformanceId == performance.PerformanceId));
                AllPerformances.Remove(performance);
                if (findBooking != null)
                    if (!findBooking.BookingPerformances.Any())
                    {
                        _bookingService.Bookings.Remove(findBooking);
                        AllBookings.Remove(findBooking);
                    }

                //List<Booking> anyPerfromances = _bookingService.Bookings.Where(booking => !booking.BookingPerformances.Any(bp => bp.Performance != null)).ToList();
                //if (anyPerfromances.Any())
                //    foreach (var booking in anyPerfromances)
                //        _bookingService.Bookings.Remove(booking);
            }
            _ = UpdatePrice();
        }


        [RelayCommand]
        private async Task GoBack()
        {
            //await Shell.Current.GoToAsync(nameof(CheckoutPage));
        }

        [RelayCommand]
        private async Task Continue()
        {
            //await Shell.Current.GoToAsync(nameof(CheckoutPage));
        }

       
        //[RelayCommand]
        //public void ShowPopup()
        //{
        //    PopupView? popup = new PopupView(this);
        //    Application.Current?.MainPage?.ShowPopup(popup);
        //}
    }
}
