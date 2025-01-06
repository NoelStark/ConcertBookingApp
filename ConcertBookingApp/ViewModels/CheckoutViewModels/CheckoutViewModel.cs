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

        public async Task Initialize()
        {
            UpdatePrice();
            allConcerts = await _concertService.GetAllConcerts();
            LoadBookings();
        }

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

                    //AddTicketsVisible = bookingPerformance.Performance.AvailableSeats > 0;
                }
            //var hasse = FlattenedBookingPerformances.Where(a => a.SeatsBooked == 0).ToList();
            
            //foreach (var items in hasse)
            //{
            //    int index = 0;
            //    index = (FlattenedBookingPerformances.IndexOf(items));
            //    FlattenedBookingPerformances.RemoveAt(index);
            //    AddTicketsVisible = false;
            //    FlattenedBookingPerformances.Insert(index, items);
            //}

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
