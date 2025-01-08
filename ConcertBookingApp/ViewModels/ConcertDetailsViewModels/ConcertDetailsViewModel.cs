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
        private readonly IMapper _mapper;

        /// <summary>
        /// Deserializes a json string into a concert object to preserve object references and also 
        /// trigger the loading of performances for the selected concert
        /// </summary>
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
                _= LoadPerfomances(); //Callas on th emethod to load perofrmances
            }
        }

        public ConcertDetailsViewModel(BookingService bookingservice, UserService userService, ConcertService concertService, IMapper mapper)
        {
            bookingService = bookingservice;
            _concertService = concertService;
            _mapper = mapper;
            _userService = userService;
        }

        /// <summary>
        /// This method loads all the perfromances for a selected concert, its uses mapping to only
        /// allow the nessesary attributes for relevance of the perfromances.
        /// 
        /// It fills the AllPerformancesForConcert, and also each perfromances availible seats
        /// </summary>
        /// <returns></returns>
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

            foreach(var performance in AllPerformancesForConcert)
            { 
                var hasse = bookingService.CurrentBooking.BookingPerformances.FirstOrDefault(a => a.Performance.PerformanceId == performance.Performance.PerformanceId);
                if (hasse != null)
                {
                    performance.Performance.AvailableSeats -= hasse.SeatsBooked;
                }
            }
            //This section makes sure that the dates for the concerts to be visible to the gui
            OnPropertyChanged(nameof(AllPerformancesForConcert));
            Performance =AllPerformancesForConcert[0].Performance;
            Performance.Date = AllPerformancesForConcert[0].Performance.Date;
            Performance.Location = AllPerformancesForConcert[0].Performance.Location;
            Date =
                $"{AllPerformancesForConcert[0].Performance.Date.ToString("dd MMM yyyy")} - {AllPerformancesForConcert[AllPerformancesForConcert.Count-1].Performance.Date.ToString("dd MMM yyyy")}";
            OnPropertyChanged(nameof(Date));
            UpdateButton();
        }

        /// <summary>
        /// This method updates the button to be clickable if the user have chosen any perfromances (seatsbooked)
        /// </summary>
        private void UpdateButton()
        {
            List<BookingPerformance> result = AllPerformancesForConcert.Where(x => x.SeatsBooked > 0).ToList();
            CanBeClicked = result.Any();
        }

        /// <summary>
        /// This resets the button after adding the items to the cart
        /// </summary>
        /// <returns></returns>
        private async Task ResetCartButton()
        {
            await Task.Delay(2000);
            AddedToCart = false;
        }
    }
}
