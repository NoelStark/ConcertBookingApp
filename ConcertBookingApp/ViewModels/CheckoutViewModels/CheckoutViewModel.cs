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
using ConcertBookingApp.Views;
using SharedResources.DTOs;
using SharedResources.Models;
using ConcertBookingApp.Data.Database;
using CommunityToolkit.Mvvm.Input;


namespace ConcertBookingApp.ViewModels.CheckoutViewModels
{
    public partial class CheckoutViewModel : ObservableObject
    {
        private readonly BookingService _bookingService;
        private readonly ConcertService _concertService;
        private readonly IMapper _mapper;
        
       
        public CheckoutViewModel(ConcertService concertService,BookingService bookingService, IMapper mapper)
        {
            _bookingService = bookingService;
            _concertService = concertService;
            _mapper = mapper;
            _= Initialize();
        }

        private async Task Initialize()
        {
            UpdatePrice();
            allConcerts = await _concertService.GetAllConcerts();
            LoadBookings();
        }

        private void FillFlattenedPerformances()
        {
            Concert concert = new Concert();
            FlattenedBookingPerformances.Clear();
            foreach (Booking booking in AllBookings)
            {
                foreach (BookingPerformance? bookingPerformance in booking.BookingPerformances)
                {
                    concert = SelectedConcerts.FirstOrDefault(x => x.ConcertId == bookingPerformance.Performance.ConcertId);
                    FlattenedBookingPerformances.Add(new BookingPerformance
                    {
                        Performance = bookingPerformance.Performance,
                        SeatsBooked = bookingPerformance.SeatsBooked,
                        ImageURL = concert.ImageUrl,
                        Genre = concert.Genre,
                        Title = concert.Name
                    });
                    
                    AddTicketsVisible = bookingPerformance.Performance.AvailableSeats > 0;
                }
            }
            UpdateNextButton();
        }
        
        public void LoadBookings()
        {
            AllBookings.Clear();
            AllBookings.Add(_bookingService.CurrentBooking);

            List<Performance> findPerformances = _bookingService.CurrentBooking.BookingPerformances.Select(a => a.Performance).ToList();
            //List<BookingPerformance> findBookingPerformances = AllBookings.SelectMany(a => a.BookingPerformances).ToList();
            List<Concert> matchingConcerts = allConcerts.Where(a => findPerformances.Any(b => b.ConcertId == a.ConcertId)).ToList();

            foreach (Concert concertDTO in matchingConcerts)
            {
                var concert = _mapper.Map<Concert>(concertDTO);
                SelectedConcerts.Add(concert);
            }
            FillFlattenedPerformances();
        }

        private void UpdatePrice()
        {
            if (_bookingService.CurrentBooking != null)
            {
                TotalPrice = _bookingService.CurrentBooking.BookingPerformances.Sum(x => x.SeatsBooked * x.Performance.Price);
                TotalAmountOfItems = _bookingService.CurrentBooking.BookingPerformances.Sum(b => b.SeatsBooked);
            }
            else
            {
                TotalPrice = 0;
                TotalAmountOfItems = 0;
            }
        }
        private void UpdateNextButton()
        {
            if (FlattenedBookingPerformances.Any())
                CanBeClicked = true;
            else
                CanBeClicked = false;
        }
    }
}


//private void LoadBookings()
//{
//BookingsCart.Clear();

//AllPerformances.Clear();
//SelectedConcerts.Clear();

//BookingsCart.Add(_bookingService.CurrentBooking);
//if (BookingsCart.Any())
//    CanBeClicked = true;
//AllBookings.Clear();
//AllBookings.Add(_bookingService.CurrentBooking);

//List<Performance> findPerformances = AllBookings
//    .SelectMany(a => a.BookingPerformances)
//    .Select(a => a.Performance)
//    .ToList();
//List<BookingPerformance> findBookingPerformances = AllBookings.SelectMany(a => a.BookingPerformances).ToList();
//List<Concert> matchingConcerts = allConcerts
//    .Where(a => findPerformances.Any(b => b.ConcertId == a.ConcertId))
//    .ToList();
//foreach (Concert concertDTO in matchingConcerts)
//{
//    var concert = _mapper.Map<Concert>(concertDTO);
//    SelectedConcerts.Add(concert);
//    foreach(var performance in findPerformances)
//        AllPerformances.Add(performance);

//}

//FillFlattenedPerformances();

//var concertModels = _mapper.Map<List<Concert>>(SelectedConcerts);
//var performanceModels = _mapper.Map<List<Performance>>(AllPerformances);

//foreach (var bookingList in BookingsCart)
//        foreach (var bookingPerformance in bookingList.BookingPerformances)
//            foreach (var concert in concertModels)
//            {
//                Performance foundPerfromance = concert.Performances.FirstOrDefault(a => a.PerformanceId == bookingPerformance.Performance.PerformanceId);
//                foundPerfromance.BookingPerformance = bookingPerformance;
//            }

//}

//namespace ConcertBookingApp.ViewModels
//{
//    public partial class CheckoutViewModel : ObservableObject
//    {
//        [ObservableProperty]
//        private double totalPrice = 0;

//        [ObservableProperty]
//        private int totalAmountOfItems = 0;

//        [ObservableProperty]
//        private bool canBeClicked = false;

//        [ObservableProperty]
//        private bool addTicketsVisible = true;

//        [ObservableProperty]
//        private Concert concert;

//        [ObservableProperty]
//        private Performance performance;

//        [ObservableProperty]
//        private Booking booking;

//        [ObservableProperty]
//        private BookingPerformance bookingPerformance;

//        private readonly BookingService _bookingService;
//        private readonly ConcertService _concertService;

//        private readonly IMapper _mapper;
//        //public ObservableCollection<Booking> BookingsCart { get; set; } = new ObservableCollection<Booking>(); //toabort
//        public static ObservableCollection<Booking> AllBookings { get; set; } = new ObservableCollection<Booking>(); //Ta bort
//        public List<Concert> allConcerts;


//        public ObservableCollection<BookingPerformance> FlattenedBookingPerformances { get; set; } = new ObservableCollection<BookingPerformance>();
//        public ObservableCollection<Concert> SelectedConcerts { get; set; } = new ObservableCollection<Concert>();
//        //public ObservableCollection<Performance> AllPerformances { get; set; } = new ObservableCollection<Performance>();
//        public CheckoutViewModel(ConcertService concertService, BookingService bookingService, IMapper mapper)
//        {
//            _bookingService = bookingService;
//            _concertService = concertService;
//            _mapper = mapper;
//            Initialize();
//        }

//        private async Task Initialize()
//        {
//            UpdatePrice();
//            allConcerts = await _concertService.GetAllConcerts();
//            LoadBookings();
//        }

//        private void FillFlattenedPerformances()
//        {
//            Concert concert = new Concert();
//            foreach (Booking booking in AllBookings)
//            {
//                foreach (BookingPerformance? bookingPerformance in booking.BookingPerformances)
//                {
//                    concert = SelectedConcerts.FirstOrDefault(x => x.ConcertId == bookingPerformance.Performance.ConcertId);
//                    //bookingPerformance.Performance.Concert = concert;
//                    FlattenedBookingPerformances.Add(new BookingPerformance
//                    {
//                        Performance = bookingPerformance.Performance,
//                        SeatsBooked = bookingPerformance.SeatsBooked,
//                        ImageURL = concert.ImageUrl,
//                        Genre = concert.Genre,
//                        Title = concert.Name
//                    });

//                    AddTicketsVisible = bookingPerformance.Performance.AvailableSeats > 0;
//                }
//            }
//        }

//        public void LoadBookings()
//        {
//            AllBookings.Clear();
//            AllBookings.Add(_bookingService.CurrentBooking);
//            if (AllBookings.Any())
//                CanBeClicked = true;

//            List<Performance> findPerformances = _bookingService.CurrentBooking.BookingPerformances.Select(a => a.Performance).ToList();
//            //List<BookingPerformance> findBookingPerformances = AllBookings.SelectMany(a => a.BookingPerformances).ToList();
//            List<Concert> matchingConcerts = allConcerts.Where(a => findPerformances.Any(b => b.ConcertId == a.ConcertId)).ToList();

//            foreach (Concert concertDTO in matchingConcerts)
//            {
//                var concert = _mapper.Map<Concert>(concertDTO);
//                SelectedConcerts.Add(concert);
//                //foreach (var performance in findPerformances)
//                //    AllPerformances.Add(performance);
//            }
//            FillFlattenedPerformances();
//        }

//        private void UpdatePrice()
//        {
//            if (_bookingService.CurrentBooking != null)
//            {
//                TotalPrice = _bookingService.CurrentBooking.BookingPerformances.Sum(x => x.SeatsBooked * x.Performance.Price);
//                TotalAmountOfItems = _bookingService.CurrentBooking.BookingPerformances.Sum(b => b.SeatsBooked);
//            }
//            else
//            {
//                TotalPrice = 0;
//                TotalAmountOfItems = 0;
//            }
//        }

//        [RelayCommand]
//        void IncreaseQuantity(BookingPerformance performance)
//        {
//            BookingPerformance chosenPerformance = _bookingService.CurrentBooking.BookingPerformances.FirstOrDefault(b => b.Performance.PerformanceId == performance.Performance.PerformanceId);

//            chosenPerformance.SeatsBooked++;
//            chosenPerformance.Performance.AvailableSeats--;
//            //if (chosenPerformance.Performance.AvailableSeats == 0)
//            //    TicketsLeft(chosenPerformance);
//            FlattenedBookingPerformances.Clear();
//            FillFlattenedPerformances();
//            UpdatePrice();
//        }

//        [RelayCommand]
//        void DecreaseQuantity(BookingPerformance performanceDTO)
//        {
//            BookingPerformance chosenPerformance = _bookingService.CurrentBooking.BookingPerformances.FirstOrDefault(b => b.Performance.PerformanceId == performanceDTO.Performance.PerformanceId);

//            chosenPerformance.SeatsBooked--;
//            chosenPerformance.Performance.AvailableSeats++;
//            //AddTicketsVisible = true;
//            if (chosenPerformance.SeatsBooked == 0)
//            {
//                Booking findBooking = _bookingService.CurrentBooking;
//                findBooking.BookingPerformances.Remove(chosenPerformance);
//                if (!findBooking.BookingPerformances.Any())
//                    _bookingService.CurrentBooking = null;
//            }
//            FlattenedBookingPerformances.Clear();
//            FillFlattenedPerformances();
//            UpdatePrice();
//        }


//        [RelayCommand]
//        private async Task GoBack()
//        {
//            await Shell.Current.GoToAsync("///ConcertOverviewPage");
//        }

//        [RelayCommand]
//        private async Task Continue()
//        {
//            await Shell.Current.GoToAsync(nameof(PaymentPage));
//        }
//    }
//}
