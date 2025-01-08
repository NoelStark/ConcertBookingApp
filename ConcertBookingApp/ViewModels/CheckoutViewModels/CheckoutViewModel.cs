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
        /// <summary>
        /// Initialize the view with all relevent properties etc
        /// </summary>
        /// <returns></returns>
        public async Task Initialize()
        {
            UpdatePrice();
            allConcerts = await _concertService.GetAllConcerts();
            LoadBookings();
        }

        /// <summary>
        /// Populates FlattenedBookingPerformances by mapping data from BookingPerformances 
        /// and their related Concert objects. It ensuring that the GUI updates correct with each performances
        /// that is in cart
        /// </summary>
        private void FillFlattenedPerformances()
        {
            Concert concert = new Concert();
            FlattenedBookingPerformances.Clear();

            foreach (BookingPerformance? bookingPerformance in _bookingService.CurrentBooking.BookingPerformances)
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
            }
            OnPropertyChanged(nameof(FlattenedBookingPerformances));
            UpdateNextButton();
            
        }
        /// <summary>
        /// Loadbookings method loads all the selected concerts by checking all the performancs the user
        /// have added to cart.
        /// </summary>
        public void LoadBookings()
        {
            List<Performance> findPerformances = _bookingService.CurrentBooking.BookingPerformances.Select(a => a.Performance).ToList();
            List<Concert> matchingConcerts = allConcerts.Where(a => findPerformances.Any(b => b.ConcertId == a.ConcertId)).ToList();

            foreach (Concert concert in matchingConcerts)
            {
                SelectedConcerts.Add(concert);
            }
            FillFlattenedPerformances();
        }

        /// <summary>
        /// This method updates the current price in the cart
        /// It takes each performance seatsbooked * the performance price. It also calculates the total perfromances in the cart.
        /// If theres no items in cart, the it sets the values to 0
        /// </summary>
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
        /// <summary>
        /// This method updates the button that it's clickable if user have any performances in cart
        /// </summary>
        private void UpdateNextButton()
        {
            if (FlattenedBookingPerformances.Any())
                CanBeClicked = true;
            else
                CanBeClicked = false;
        }
    }
}
