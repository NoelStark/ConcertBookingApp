using System;
using System.Collections;
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
        /// <summary>
        /// Method to retrieve all concerts
        /// Gets a list of ConcertDTO objects from the API and then maps them to Concert objects and then returns the list.
        /// </summary>
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

        /// <summary>
        /// Method to get all available seats.
        /// Returns a dictionary where the key represents the concert and the value represents the number of available seats
        /// </summary>
        public async Task<Dictionary<int,int>> GetAvailableSeats()
        {
            var response = await _httpClient.GetAsync("Concerts/GetAvailableSeats");
            Dictionary<int, int> availableSeats = await response.Content.ReadFromJsonAsync<Dictionary<int, int>>();
            return availableSeats;
        }

        /// <summary>
        /// Method to get all performances for a selected concert
        /// Takes a concert id as a parameter to retrieve the correct performances associated with the specified concert
        /// gets a list of PerformanceDTO objects and then maps them to Performance objects and returns the list
        /// </summary>
        public async Task<List<Performance>> GetPerformancesForConcert(int concertId)
        {
            var response = await _httpClient.GetAsync($"Concerts/{concertId}");
            List<Performance> performances= _mapper.Map<List<Performance>>(await response.Content.ReadFromJsonAsync<List<PerformanceDTO>>());
            return performances;
        }

        /// <summary>
        /// Method to retriev a specific performance, takes a performance id as a parameter to retrieve the correct corresponding performance
        /// Gets a PerformanceDTO object and then maps it to a Performance object which then returns the object
        /// </summary>
        public async Task<Performance> GetPerformance(int performanceId)
        {
            var response = await _httpClient.GetAsync($"Concerts/Performance/{performanceId}");
            Performance performance =
                _mapper.Map<Performance>(await response.Content.ReadFromJsonAsync<PerformanceDTO>());
            return performance;
        }
        /// <summary>
        /// Method to get the correct concert for a selected performance, takes a performance id as a parameter to get the correcct corresponding concert
        /// Sends an HTTP GET request to the api, then gets a ConcertDTO object which will be maped to a Concert object and returns the object.
        /// </summary>
        public async Task<Concert> GetConcertForPerformance(int performanceId)
        {
            var response = await _httpClient.GetAsync($"Concerts/GetPerformances/{performanceId}");
            Concert concert = _mapper.Map<Concert>(await response.Content.ReadFromJsonAsync<ConcertDTO>());

            return concert;
        }
    }
}
