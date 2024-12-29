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
using AutoMapper;
using ConcertBookingApp.DTOs;
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
        private readonly ConcertService _concertService;

        private readonly IMapper _mapper;
        //private static ConcertOverviewViewModel _concertViewModel;

        public ObservableCollection<List<Booking>> BookingsCart { get; set; } = new ObservableCollection<List<Booking>>();
        public static ObservableCollection<Booking> AllBookings { get; set; } = new ObservableCollection<Booking>();
        public List<ConcertDTO> allConcerts;

        public ObservableCollection<BookingPerformance> FlattenedBookingPerformances { get; set; } =
            new ObservableCollection<BookingPerformance>();
        public ObservableCollection<Concert> SelectedConcerts { get; set; } = new ObservableCollection<Concert>();
        public ObservableCollection<PerformanceDTO> AllPerformances { get; set; } = new ObservableCollection<PerformanceDTO>();
        public CheckoutViewModel(ConcertService concertService,BookingService bookingService, IMapper mapper)
        {
            _bookingService = bookingService;
            _concertService = concertService;
            _mapper = mapper;
            Initialize();
        }

        private async Task Initialize()
        {
            allConcerts = await _concertService.GetAllConcerts();
            LoadBookings();
            UpdatePrice();
            FillFlattenedPerformances();
        }

        private void FillFlattenedPerformances()
        {
            foreach (Booking booking in AllBookings)
            {
                foreach (BookingPerformance bookingPerformance in booking.BookingPerformances)
                {
                    var concert = SelectedConcerts
                        .FirstOrDefault(x => x.ConcertId == bookingPerformance.Performance.ConcertId);
                    bookingPerformance.Performance.Concert = concert;
                    FlattenedBookingPerformances.Add(new BookingPerformance
                    {
                        Performance = bookingPerformance.Performance,
                        SeatsBooked = bookingPerformance.SeatsBooked,
                        ImageURL = concert.ImageUrl,
                    });
                }
            }
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

            List<Performance> findPerformances = AllBookings
                .SelectMany(a => a.BookingPerformances)
                .Select(a => a.Performance)
                .ToList();
            List<BookingPerformance> findBookingPerformances = AllBookings.SelectMany(a => a.BookingPerformances).ToList();
            List<ConcertDTO> matchingConcerts = allConcerts
                .Where(a => findPerformances.Any(b => b.ConcertId == a.ConcertId))
                .ToList();
            foreach (ConcertDTO concertDTO in matchingConcerts)
            {
                var concert = _mapper.Map<Concert>(concertDTO);
                var performanceDTOs = _concertService.GetPerformancesForConcert(concert.ConcertId);
                var concertPerformances = _mapper.Map<List<Performance>>(performanceDTOs);
                concert.Performances = concertPerformances;
                SelectedConcerts.Add(concert);
                foreach(var performance in concertPerformances)
                    AllPerformances.Add(_mapper.Map<PerformanceDTO>(performance));

            }
            //List<PerformanceDTO> performances = _concertService.GetPerformancesForConcert(Concert.ConcertId);
            //foreach (PerformanceDTO performance in performances)
            //{
            //    //Performance performanceFound = SelectedConcerts.SelectMany(c => c.Performances).FirstOrDefault(p => p.PerformanceId == performance.PerformanceId);
            //    //if (performanceFound != null)
            //}

            var concertModels = _mapper.Map<List<Concert>>(SelectedConcerts);
            var performanceModels = _mapper.Map<List<Performance>>(AllPerformances);
            foreach (var performance in performanceModels)
                performance.Concert = concert;
            //foreach (var concert in SelectedConcerts)
            //{
            //    var concertPerformances = AllPerformances.Where(x => x.ConcertId == concert.ConcertId).ToList();
            //    foreach (var performance in concer)
            //        performance.Concert = concert;
            //}

            foreach (var bookingList in BookingsCart)
                foreach (var booking in bookingList)
                    foreach (var bookingPerformance in booking.BookingPerformances)
                        foreach (var concert in concertModels)
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
        void IncreaseQuantity(BookingPerformance performance)
        {
            //BookingPerformance findPerformances = AllBookings.SelectMany(a => a.BookingPerformances).FirstOrDefault(b => b.Performance.PerformanceId == performance.PerformanceId);
                performance.SeatsBooked++;
            performance.Performance.AvailableSeats--;
            if (performance.Performance.AvailableSeats == 0)
                AddTicketsVisible = false;
            _ = UpdatePrice();
        }

        [RelayCommand]
        void DecreaseQuantity(BookingPerformance performanceDTO)
        {
            //BookingPerformance findPerformances = AllBookings.SelectMany(a => a.BookingPerformances).FirstOrDefault(b => b.Performance.PerformanceId == performance.PerformanceId);
            //var performance = _bookingService.Bookings
            //    .SelectMany(x => x.BookingPerformances)
            //    .FirstOrDefault(x => x.Performance.PerformanceId == performanceDTO.Performance.PerformanceId);
            performanceDTO.SeatsBooked--;
            performanceDTO.Performance.AvailableSeats++;
            if (performanceDTO.SeatsBooked == 0)
            {
                Booking findBooking = _bookingService.Bookings.FirstOrDefault(a => a.Performances.Any(b => b.PerformanceId == performanceDTO.PerformanceId));
                var performanceToRemove = AllPerformances
                    .FirstOrDefault(x => x.PerformanceId == performanceDTO.Performance.PerformanceId);
                AllPerformances.Remove(performanceToRemove);
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
