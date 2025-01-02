using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using ConcertBookingApp.Services;
using ConcertBookingApp.ViewModels.ConcertsOverviewViewModels;
using SharedResources.DTOs;
using SharedResources.Models;

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
        public ObservableCollection<List<Booking>> BookingsCart { get; set; } = new ObservableCollection<List<Booking>>(); //toabort
        public static ObservableCollection<Booking> AllBookings { get; set; } = new ObservableCollection<Booking>(); //Ta bort
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
            UpdatePrice();
            allConcerts = await _concertService.GetAllConcerts();
            LoadBookings();
            CanBeClicked = false;
        }

        private void FillFlattenedPerformances()
        {
            Concert concert = new Concert();
            foreach (Booking booking in AllBookings)
            {
                foreach (BookingPerformance bookingPerformance in booking.BookingPerformances)
                {
                    concert = SelectedConcerts
                        .FirstOrDefault(x => x.ConcertId == bookingPerformance.Performance.ConcertId);
                    //bookingPerformance.Performance.Concert = concert;
                    FlattenedBookingPerformances.Add(new BookingPerformance
                    {
                        Performance = bookingPerformance.Performance,
                        SeatsBooked = bookingPerformance.SeatsBooked,
                        ImageURL = concert.ImageUrl,
                        Genre = concert.Genre,
                        Title = concert.Name
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
                SelectedConcerts.Add(concert);
                foreach(var performance in findPerformances)
                    AllPerformances.Add(_mapper.Map<PerformanceDTO>(performance));

            }

            FillFlattenedPerformances();

            var concertModels = _mapper.Map<List<Concert>>(SelectedConcerts);
            var performanceModels = _mapper.Map<List<Performance>>(AllPerformances);

            foreach (var bookingList in BookingsCart)
                foreach (var booking in bookingList)
                    foreach (var bookingPerformance in booking.BookingPerformances)
                        foreach (var concert in concertModels)
                        {
                            Performance foundPerfromance = concert.Performances.FirstOrDefault(a => a.PerformanceId == bookingPerformance.Performance.PerformanceId);
                            foundPerfromance.BookingPerformance = bookingPerformance;
                        }

        }

        private void UpdatePrice()
        {
            TotalPrice = _bookingService.Bookings.SelectMany(a => a.BookingPerformances).Sum(b => b.SeatsBooked * b.Performance.Price);
            TotalAmountOfItems = _bookingService.Bookings.SelectMany(a => a.BookingPerformances).Sum(b => b.SeatsBooked);
        }

        [RelayCommand]
        void IncreaseQuantity(BookingPerformance performance)
        {
            BookingPerformance chosenPerformance = _bookingService.Bookings.SelectMany(a => a.BookingPerformances).FirstOrDefault(b => b.Performance.PerformanceId == performance.Performance.PerformanceId);

            chosenPerformance.SeatsBooked++;
            chosenPerformance.Performance.AvailableSeats--;
            if (chosenPerformance.Performance.AvailableSeats == 0)
                AddTicketsVisible = false;

            FlattenedBookingPerformances.Clear();
            FillFlattenedPerformances();
            UpdatePrice();
        }

        [RelayCommand]
        void DecreaseQuantity(BookingPerformance performanceDTO)
        {
            BookingPerformance chosenPerformance = _bookingService.Bookings.SelectMany(a => a.BookingPerformances).FirstOrDefault(b => b.Performance.PerformanceId == performanceDTO.Performance.PerformanceId);

            chosenPerformance.SeatsBooked--;
            chosenPerformance.Performance.AvailableSeats--;
            if (chosenPerformance.SeatsBooked == 0)
            {
                Booking findBooking = _bookingService.Bookings.FirstOrDefault(a => a.BookingPerformances.Contains(chosenPerformance));
                findBooking.BookingPerformances.Remove(chosenPerformance);
                if (!findBooking.BookingPerformances.Any())
                    _bookingService.Bookings.Remove(findBooking);
            }
            FlattenedBookingPerformances.Clear();
            FillFlattenedPerformances();
            UpdatePrice();
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
