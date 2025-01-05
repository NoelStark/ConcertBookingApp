using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedResources.Models;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ConcertBookingApp.ViewModels.ConcertDetailsViewModels
{
    public partial class ConcertDetailsViewModel
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

        [ObservableProperty]
        private bool addTicketsVisible = true;

        [ObservableProperty]
        private bool canBeClicked = false;

        [ObservableProperty] private string date = string.Empty;
        [ObservableProperty] private bool addedToCart = false;

        public ObservableCollection<BookingPerformance> AllPerformancesForConcert { get; set; } = new ObservableCollection<BookingPerformance>();
    }
}
