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
using ConcertBookingApp.Views;

namespace ConcertBookingApp.ViewModels
{
    [QueryProperty(nameof(ConvertFromJson), "concert")]
    public partial class ConcertDetailsViewModel : ObservableObject
    {
        private readonly BookingService bookingService;
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

        public ObservableCollection<BookingPerformance> AllPerformancesForConcert { get; set; } =
            new ObservableCollection<BookingPerformance>();

        public string ConvertFromJson
        {
            set
            {
                Concert = JsonSerializer.Deserialize<Concert>(Uri.UnescapeDataString(value));
                Performance = Concert.Performances.FirstOrDefault(a => a.ConcertId == Concert.ConcertId);
                AmountOfTickets = 0;
                _ = LoadPerfomances();
                UpdateButton();
            }
        }

        public ConcertDetailsViewModel(BookingService bookingservice)
        {
            bookingService = bookingservice;
        }
        private async Task LoadPerfomances()
        {
            //Fast hämta från databasen
            AllPerformancesForConcert.Clear();
            List<Performance> result = Concert.Performances.Where(a => a.ConcertId.Equals(Performance.ConcertId)).ToList();
            foreach (Performance item in result)
            {
                AllPerformancesForConcert.Add(new BookingPerformance{Performance = item});
            }
            //Test.Add(new BookingPerformance
            //{
            //    Booking = booking2,
            //    Performance = AllPerformancesForConcert[0],
            //    BookingId = booking2.BookingId,
            //    PerformanceId = AllPerformancesForConcert[0].PerformanceId
            //});
            //OnPropertyChanged(nameof(AmountOfTickets));
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

            //BookingPerformance result = AllPerformancesForConcert.FirstOrDefault(a => a.PerformanceId == bookingPerformance.PerformanceId);
            //int index = AllPerformancesForConcert.IndexOf(result);
        }

        [RelayCommand]
        void DecreaseQuantity(BookingPerformance bookingPerformance)
        {
            if (bookingPerformance.SeatsBooked > 0)
            { 
                string value = "Decrease";
                bookingPerformance.SeatsBooked--;
                bookingPerformance.Performance.AvailableSeats++;
                AddTicketsVisible = true;
                UpdateButton();
            }
        }

        [RelayCommand]
        private async Task BuyTickets()
        {
            bookingService.Bookings.Add(new Booking
            {
                BookingPerformances = AllPerformancesForConcert.Where(x => x.SeatsBooked > 0).ToList()
            });
            //string serializedBookings = JsonSerializer.Serialize(bookingService.Bookings);
            //string encodedBookings = Uri.EscapeDataString(serializedBookings);
            await Shell.Current.GoToAsync(nameof(CheckoutPage));
            //await Shell.Current.GoToAsync($"///BookingsPage?bookings={encodedBookings}");
        }

        //On
    }
}
