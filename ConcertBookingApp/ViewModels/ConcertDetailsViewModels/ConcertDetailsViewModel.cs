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
using ConcertBookingApp.Data.Database;

namespace ConcertBookingApp.ViewModels.ConcertDetailsViewModels
{
    [QueryProperty(nameof(ConvertFromJson), "concert")]
    public partial class ConcertDetailsViewModel : ObservableObject
    {
        private readonly BookingService bookingService;
        private readonly ConcertService _concertService;
        private readonly UnitOfWork _unitOfWork;
        private readonly UserService _userService;

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
                _= LoadPerformances();

            }
        }

        private readonly IMapper _mapper;
        public ConcertDetailsViewModel(BookingService bookingservice, UserService userService, ConcertService concertService, IMapper mapper)
        {
            bookingService = bookingservice;
            _concertService = concertService;
            _mapper = mapper;
            _userService = userService;
        }
        private async Task LoadPerformances()
        {
           
            AllPerformancesForConcert.Clear();

            var performancesDTO = await _concertService.GetPerformancesForConcert(Concert.ConcertId);

            var tempPerformances = performancesDTO.Select(performanceDTO => new BookingPerformance
            {
                Performance = _mapper.Map<Performance>(performanceDTO)
            }).ToList();

            foreach (var performance in tempPerformances)
            {
                var t = new List<BookingPerformance>();
                if (bookingService.CurrentBooking != null)
                {

                    foreach (var booking in bookingService.CurrentBooking.BookingPerformances)
                    {
                        if (performance.Performance.PerformanceId == booking.PerformanceId)
                        {
                            performance.Performance.AvailableSeats -= booking.SeatsBooked;
                        }
                    }
                }

                
                AllPerformancesForConcert.Add(performance);
               
            }


            OnPropertyChanged(nameof(AllPerformancesForConcert));
            Performance =AllPerformancesForConcert[0].Performance;
            Performance.Date = AllPerformancesForConcert[0].Performance.Date;
            Performance.Location = AllPerformancesForConcert[0].Performance.Location;
            Date =
                $"{AllPerformancesForConcert[0].Performance.Date.ToString("dd MMM yyyy")} - {AllPerformancesForConcert[AllPerformancesForConcert.Count-1].Performance.Date.ToString("dd MMM yyyy")}";
            OnPropertyChanged(nameof(Date));
            UpdateButton();
        }

        private void UpdateButton()
        {
            List<BookingPerformance> result = AllPerformancesForConcert.Where(x => x.SeatsBooked > 0).ToList();
            CanBeClicked = result.Any();
        }

        private async Task ResetCartButton()
        {
            await Task.Delay(2000);
            AddedToCart = false;
            _ = LoadPerformances();

        }
    }
}
