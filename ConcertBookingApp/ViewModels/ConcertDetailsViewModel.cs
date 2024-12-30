using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using ConcertBookingApp.Services;
using ConcertBookingApp.Views;
using SharedResources.DTOs;
using SharedResources.Models;

namespace ConcertBookingApp.ViewModels
{
    [QueryProperty(nameof(ConvertFromJson), "concert")]
    public partial class ConcertDetailsViewModel : ObservableObject
    {
        private readonly BookingService bookingService;
        private readonly ConcertService _concertService;
        [ObservableProperty]
        private Concert concert;

        [ObservableProperty]
        private PerformanceDTO performance;

        [ObservableProperty]
        private Booking booking;

        [ObservableProperty]
        private int amountOfTickets = 0;

        [ObservableProperty]
        private double totalPrice = 0;

        [ObservableProperty]
        private bool addTicketsVisible = true;

        [ObservableProperty]
        private bool canBeClicked = false;

        public ObservableCollection<BookingPerformance> AllPerformancesForConcert { get; set; } =
            new ObservableCollection<BookingPerformance>();

        public string ConvertFromJson
        {
            set
            {
                string decoded = Uri.UnescapeDataString(value);
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                };
                var concertDTO = JsonSerializer.Deserialize<ConcertDTO>(decoded,options);
                Concert = _mapper.Map<Concert>(concertDTO);
                GetPerformances(Concert.ConcertId);
               
            }
        }

        private async Task GetPerformances(int id)
        {
            var performanceDTOs = await _concertService.GetPerformancesForConcert(id);
            Performance = performanceDTOs.FirstOrDefault(x => x.ConcertId == Concert.ConcertId);
            Concert.Performances = _mapper.Map<List<Performance>>(performanceDTOs);
            AmountOfTickets = 0;
            LoadPerfomances();
            UpdateButton();
        }

        private readonly IMapper _mapper;
        public ConcertDetailsViewModel(BookingService bookingservice, ConcertService concertService, IMapper mapper)
        {
            bookingService = bookingservice;
            _concertService = concertService;
            _mapper = mapper;
        }
        private void LoadPerfomances()
        {
            AllPerformancesForConcert.Clear();
            List<Performance> result = Concert.Performances.Where(a => a.ConcertId.Equals(Performance.ConcertId)).ToList();
            foreach (Performance item in result)
            {
                AllPerformancesForConcert.Add(new BookingPerformance{Performance = item});
            }
        }

        private void UpdateButton()
        {
            List<BookingPerformance> result = AllPerformancesForConcert.Where(x => x.SeatsBooked > 0).ToList();
            if(result.Any())
                CanBeClicked = true;
            else 
                CanBeClicked = false;
        }
       

        [RelayCommand]
        void IncreaseQuantity(BookingPerformance bookingPerformance)
        {
            string value = "Increase";
            bookingPerformance.SeatsBooked++;
            bookingPerformance.Performance.AvailableSeats--;
            if(bookingPerformance.Performance.AvailableSeats == 0)
                AddTicketsVisible = false;
            UpdateButton();
        }

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

        [RelayCommand]
        private async Task BuyTickets()
        {
            List<BookingPerformance> hasse = AllPerformancesForConcert.Where(x => x.SeatsBooked > 0).ToList();
            if (bookingService.Bookings.Any())
            {
                Booking currentBooking = bookingService.Bookings[0];
                foreach (var performance in hasse)
                {
                    currentBooking.BookingPerformances.Add(performance);
                }
            }
            else
            {
                bookingService.Bookings.Add(new Booking
                {
                    BookingPerformances = new List<BookingPerformance>(hasse)

                });
            }

            //currentBooking.BookingPerformances.Add(new List<BookingPerformance>(hasse));
            //bookingService.Bookings.Add(new Booking
            //{
            //    BookingPerformances = new List<BookingPerformance>(hasse)

            //});
            //bookingService.Bookings.Add(new Booking
            //{
            //    BookingPerformances = AllPerformancesForConcert.Where(x => x.SeatsBooked > 0).ToList()
            //});
            await Shell.Current.GoToAsync(nameof(CheckoutPage));
        }
    }
}
