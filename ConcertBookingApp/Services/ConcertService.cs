using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SharedResources.DTOs;
using SharedResources.Models;

namespace ConcertBookingApp.Services
{
    public class ConcertService
    {
        private readonly IMapper _mapper;
        //private readonly ConcertRepository _concertRepository;
        private readonly HttpClient _httpClient;
        public ConcertService(IMapper mapper, HttpClient httpClient)
        {
            //_concertRepository = concertRepository;
            _mapper = mapper;
            _httpClient = httpClient;
        }


        public async Task<List<Concert>> GetAllConcerts()
        {
            try
            {
                var response = await _httpClient.GetAsync("Concerts"); 
                var concertDTOs = await response.Content.ReadFromJsonAsync<List<ConcertDTO>>();
                List<Concert> concerts = _mapper.Map<List<Concert>>(concertDTOs);
                return concerts;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
            }

            return new List<Concert>();
        }
        public async Task<Dictionary<int,int>> GetAvailableSeats()
        {
            var response = await _httpClient.GetAsync("Concerts/GetAvailableSeats");
            Dictionary<int, int> availableSeats = await response.Content.ReadFromJsonAsync<Dictionary<int, int>>();
            return availableSeats;
        }

        public async Task<List<Performance>> GetPerformancesForConcert(int concertId)
        {
            var response = await _httpClient.GetAsync($"Concerts/{concertId}");
            List<Performance> performances= _mapper.Map<List<Performance>>(await response.Content.ReadFromJsonAsync<List<PerformanceDTO>>());
            return performances;
        }

        public async Task<Performance> GetPerformance(int performanceId)
        {
            var response = await _httpClient.GetAsync($"Concerts/Performance/{performanceId}");
            Performance performance =
                _mapper.Map<Performance>(await response.Content.ReadFromJsonAsync<PerformanceDTO>());
            return performance;
        }
        public async Task<Concert> GetConcertForPerformance(int performanceId)
        {
            var response = await _httpClient.GetAsync($"Concerts/GetPerformances/{performanceId}");
            Concert concert = _mapper.Map<Concert>(await response.Content.ReadFromJsonAsync<ConcertDTO>());

            return concert;
        }
    }
}
