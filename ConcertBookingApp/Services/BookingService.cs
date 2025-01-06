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
        //private readonly ConcertRepository _concertRepository;
        private readonly HttpClient _httpClient;
        public BookingService(IMapper mapper, HttpClient httpClient)
        {
            //_concertRepository = concertRepository;
            _mapper = mapper;
            _httpClient = httpClient;
        }
        public Booking? CurrentBooking { get; set; } =  null;

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

        public async Task<int> SaveBooking(Booking booking)
        {
            try
            {
                var bookingDTO = _mapper.Map<BookingDTO>(booking);
                var content = JsonSerializer.Serialize(bookingDTO);
                var httpContent = new StringContent(content, Encoding.UTF8, "application/json");
                var bookingId = await _httpClient.PostAsync("Booking", httpContent);
                return int.Parse(await bookingId.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return 0;
        }
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

        public async Task<string> CancelBooking(int bookingPerformanceId, int bookingId)
        {
            var response = await _httpClient.GetAsync($"Booking/CancelPerformance/{bookingPerformanceId}/{bookingId}");
            return response.StatusCode.ToString();
        }
    }
}
