using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ConcertBookingApp.Data;
using ConcertBookingApp.DTOs;
using ConcertBookingApp.Models;

namespace ConcertBookingApp.Services
{
    public class ConcertService
    {
        private readonly IMapper _mapper;
        private readonly ConcertRepository _concertRepository;
        private readonly HttpClient _httpClient;
        public ConcertService(ConcertRepository concertRepository,IMapper mapper, HttpClient httpClient)
        {
            _concertRepository = concertRepository;
            _mapper = mapper;
            _httpClient = httpClient;
        }


        public async Task<List<ConcertDTO>> GetAllConcerts()
        {
            try
            {
                var response = await _httpClient.GetAsync("Concerts");
                Console.WriteLine(response.Content);
                var concerst = await response.Content.ReadFromJsonAsync<List<ConcertDTO>>();
                return concerst;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
            }

            return new List<ConcertDTO>();
            //var concerts = _concertRepository.GetAllConcerts();
            //return _mapper.Map<List<ConcertDTO>>(concerts);
        }
        public ConcertDTO GetConcertById(int concertId)
        {
            Concert concert= _concertRepository.GetAllConcerts().FirstOrDefault(x => x.ConcertId == concertId);
            return _mapper.Map<ConcertDTO>(concert);
        }

        public List<PerformanceDTO> GetPerformancesForConcert(int concertId)
        {
            List<Performance> performances = _concertRepository.GetPerformances(concertId);
            return _mapper.Map<List<PerformanceDTO>>(performances);
        }
    }
}
