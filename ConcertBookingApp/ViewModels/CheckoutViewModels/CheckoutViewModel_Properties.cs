using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedResources.Models;
using System.Collections.ObjectModel;

namespace ConcertBookingApp.ViewModels.CheckoutViewModels
{
    public partial class CheckoutViewModel
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

        [ObservableProperty]
        private List<Booking> allBookings = new List<Booking>();

        private List<Concert> allConcerts;
        public ObservableCollection<BookingPerformance> FlattenedBookingPerformances { get; set; } = new ObservableCollection<BookingPerformance>();
        public ObservableCollection<Concert> SelectedConcerts { get; set; } = new ObservableCollection<Concert>();
    }
}
