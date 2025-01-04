using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        private Performance performance;

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

        [ObservableProperty] private string date;
        [ObservableProperty] private bool addedToCart;

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
                var concertDTO = JsonSerializer.Deserialize<Concert>(decoded,options);
                Concert = concertDTO;
                _= LoadPerfomances();

            }
        }

        private readonly IMapper _mapper;
        public ConcertDetailsViewModel(BookingService bookingservice, ConcertService concertService, IMapper mapper)
        {
            bookingService = bookingservice;
            _concertService = concertService;
            _mapper = mapper;
        }
        private async Task LoadPerfomances()
        {
           
            AllPerformancesForConcert.Clear();

            var performancesDTO = await _concertService.GetPerformancesForConcert(Concert.ConcertId);

            var tempPerformances = performancesDTO.Select(performanceDTO => new BookingPerformance
            {
                Performance = _mapper.Map<Performance>(performanceDTO)
            }).ToList();

            foreach (var performance in tempPerformances)
            {
                AllPerformancesForConcert.Add(performance);
            }
            OnPropertyChanged(nameof(AllPerformancesForConcert));
            Performance =AllPerformancesForConcert[0].Performance;
            Performance.Date = AllPerformancesForConcert[0].Performance.Date;
            Performance.Location = AllPerformancesForConcert[0].Performance.Location;
            Date =
                $"{AllPerformancesForConcert[0].Performance.Date.ToString("dd MMM yyyy")} - {AllPerformancesForConcert[AllPerformancesForConcert.Count-1].Performance.Date.ToString("dd MMM yyyy")}";
            UpdateButton();
        }

        private void UpdateButton()
        {
            List<BookingPerformance> result = AllPerformancesForConcert.Where(x => x.SeatsBooked > 0).ToList();
            CanBeClicked = result.Any();
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
            if (bookingService.CurrentBooking != null)
            {
                Booking currentBooking = bookingService.CurrentBooking;
                foreach (var performance in hasse)
                {
                    currentBooking.BookingPerformances.Add(performance);
                }
            }
            else
            {
                bookingService.CurrentBooking = new Booking
                {
                    BookingPerformances = new List<BookingPerformance>(hasse)

                };
            }

            AddedToCart = true;
            _ = ResetCartButton();
            //await Shell.Current.GoToAsync(nameof(CheckoutPage));
        }

        private async Task ResetCartButton()
        {
            await Task.Delay(3000);
            AddedToCart = false;
        }
    }
}
