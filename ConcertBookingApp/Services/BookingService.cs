using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Shared_Resources.DTOs;
using SharedResources.DTOs;
using SharedResources.Models;

namespace ConcertBookingApp.Services
{
    public class BookingService
    {
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        public BookingService(IMapper mapper, HttpClient httpClient)
        {
            _mapper = mapper;
            _httpClient = httpClient;
        }
        public Booking? CurrentBooking { get; set; } =  null;

        /// <summary>
        /// Method to get all bookings for a specific user and takes the user id as a parameter
        /// sends an HTTP GET request to the api to retrieve a list of BookingDTO objects and then maps them to Booking objects
        /// which then will returns the list.
        /// </summary>
        public async Task<List<Booking>> GetAllBookings(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"Booking/{id}");
                var bookingDTOs = await response.Content.ReadFromJsonAsync<List<BookingDTO>>();
                List<Booking> bookings = _mapper.Map<List<Booking>>(bookingDTOs);
                return bookings;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
            }

            return new List<Booking>();
        }

        /// <summary>
        /// Method to save a new booking, take the booking as a parameter
        /// Maps a Booking object to a BookingDTO and then serializes it to json format, then it sends it to the api with post
        /// If the save is successfull it retrieves and returns the id of the saved booking as an integer
        /// </summary>
        public async Task<int> SaveBooking(Booking booking)
        {
            try
            {
                var bookingDTO = _mapper.Map<BookingDTO>(booking);
                var content = JsonSerializer.Serialize(bookingDTO);
                var httpContent = new StringContent(content, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("Booking", httpContent);
                var bookingId = int.Parse(await response.Content.ReadAsStringAsync());
                return bookingId;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return 0;
        }

        /// <summary>
        /// Method to save performances for a booking, it takes a list of bookingperfromances and bookingid as a parmeter
        /// it applies to the booking for each performance in the list, maps them to BookingPerformanceDTO objects and then
        /// serializes them into json format which will then be sent to the api with post
        /// </summary>
        public async Task SavePerformances(List<BookingPerformance> bookingPerformances, int bookingId)
        {
            try
            {
                bookingPerformances.ForEach(x => x.BookingId = bookingId);
                var bookingDTO = _mapper.Map<List<BookingPerformanceDTO>>(bookingPerformances);
                var content = JsonSerializer.Serialize(bookingDTO);
                var httpContent = new StringContent(content, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("Booking/performances", httpContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Method to get performances from a booking id, it takes the booking id as a parameter
        /// the parameter will be sendt as a HTTP GET request to the api
        /// Gets a list of BookingPerformanceDTO objects and then maps them to BookingPerformance objects and then returns the list.
        /// </summary>
        public async Task<List<BookingPerformance>> GetPerformancesForBooking(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"Booking/Performances/{id}");
                var bookingperformanceDTOs = await response.Content.ReadFromJsonAsync<List<BookingPerformanceDTO>>();
                List<BookingPerformance> bookingPerformances =
                    _mapper.Map<List<BookingPerformance>>(bookingperformanceDTOs);
                return bookingPerformances;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }

            return new List<BookingPerformance>();
        }

        /// <summary>
        /// This method cancels a booking for a bookingperfromance using the booking id and bookingperfromance id
        /// Sends an HTTP GET request to the api and returns the response status code
        /// </summary>
        public async Task<string> CancelBooking(int bookingPerformanceId, int bookingId)
        {
            var response = await _httpClient.GetAsync($"Booking/CancelPerformance/{bookingPerformanceId}/{bookingId}");
            return response.StatusCode.ToString();
        }
    }
}
