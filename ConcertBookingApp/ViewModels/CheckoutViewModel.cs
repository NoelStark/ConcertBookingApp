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

namespace ConcertBookingApp.ViewModels
{
    public partial class CheckoutViewModel : ObservableObject
    {
        [ObservableProperty]
        private double totalPrice;

        private readonly BookingService _bookingService;
        public ObservableCollection<List<Booking>> BookingsCart { get; set; } = new ObservableCollection<List<Booking>>();
        public CheckoutViewModel(BookingService bookingService)
        {
            _bookingService = bookingService;
            LoadBookings();
        }

        private void LoadBookings()
        {
            List<Booking> hasse = _bookingService.Bookings;
            //foreach (var booking in _bookingService.Bookings)
            //{
            //    //BookingsCart.Add(booking);
            //}
        }

        private async Task UpdatePrice()
        {

        }


        //[RelayCommand]
        //public void ShowPopup()
        //{
        //    PopupView? popup = new PopupView(this);
        //    Application.Current?.MainPage?.ShowPopup(popup);
        //}
    }
}
