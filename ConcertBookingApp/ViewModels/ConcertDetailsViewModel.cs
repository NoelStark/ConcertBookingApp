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

namespace ConcertBookingApp.ViewModels
{
    [QueryProperty(nameof(ConvertFromJson), "concert")]
    public partial class ConcertDetailsViewModel : ObservableObject
    {

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

       // private ObservableCollection<Performance> SelectedTickets { get; set; } = new ObservableCollection<Performance>();
       //private ObservableCollection<Booking> Bookings { get; set; } = new ObservableCollection<Booking>();

        //public ObservableCollection<Performance> AllPerformancesForConcert { get; set; } = new ObservableCollection<Performance>();

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
            }
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

        private void UpdatePrice(Performance performance, string value)
        {
            //if (value == "Increase")
            //    SelectedTickets.Add(performance);
            //else if (value == "Decrease")
            //    SelectedTickets.Remove(performance);

            //Performance? doesexist = SelectedTickets.FirstOrDefault(a => a.PerformanceId.Equals(performance.PerformanceId));

            //if (doesexist != null && value == "Increase")
            //    SelectedTickets.Add(performance);
            //else if (doesexist != null && value == "Decrease")
            //    SelectedTickets.Remove(performance);
            //else if (value == "Increase")
            //    SelectedTickets.Add(performance);
        }
       

        [RelayCommand]
        void IncreaseQuantity(BookingPerformance bookingPerformance)
        {
            string value = "Increase";
            bookingPerformance.SeatsBooked++;
            UpdatePrice(bookingPerformance.Performance, value);

        }

        [RelayCommand]
        void DecreaseQuantity(BookingPerformance bookingPerformance)
        {
            if (bookingPerformance.SeatsBooked > 0)
            { 
                string value = "Decrease";
                bookingPerformance.SeatsBooked--;
                UpdatePrice(bookingPerformance.Performance, value);
            }
        }

        [RelayCommand]
        private async Task BuyTickets()
        {
           
            BookingService bookingService = new BookingService();
            bookingService.Bookings.Add(new Booking
            {
                BookingPerformances = AllPerformancesForConcert.Where(x => x.SeatsBooked > 0).ToList()
            });
            //string serializedTickets = JsonSerializer.Serialize(SelectedTickets);
            //string encodedTickets = Uri.EscapeDataString(serializedTickets);
            await Shell.Current.GoToAsync($"///BookingsPage");
            

        }

        //On
    }
}
