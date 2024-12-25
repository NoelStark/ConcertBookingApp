using System;
using System.Collections.Generic;
using System.Linq;
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

        public ConcertService(ConcertRepository concertRepository,IMapper mapper)
        {
            _concertRepository = concertRepository;
            _mapper = mapper;
        }


        public List<ConcertDTO> GetAllConcerts()
        {
            var concerts = _concertRepository.GetAllConcerts();
            return _mapper.Map<List<ConcertDTO>>(concerts);
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
