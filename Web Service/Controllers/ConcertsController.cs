using AutoMapper;
using ConcertBookingApp.Data.Database;
using Microsoft.AspNetCore.Mvc;
using SharedResources.Data;
using SharedResources.DTOs;
using SharedResources.Models;

namespace WebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConcertsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;
        public ConcertsController(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetConcerts()
        {
            try
            {
                var concerts = await _unitOfWork.Concert.GetAllConcerts();
                List<Performance> performances = await _unitOfWork.Performance.GetAllPerformances();
                foreach (var concert in concerts)
                {
                    concert.Performances = performances.Where(x => x.ConcertId == concert.ConcertId).ToList();
                }
                return Ok(_mapper.Map<List<ConcertDTO>>(concerts));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerformancesForConcert(int id)
        {
            List<Performance> performances = await _unitOfWork.Performance.GetPerformancesForConcert(id);
            return Ok(_mapper.Map<List<PerformanceDTO>>(performances));
        }

        [HttpGet("performance/{performanceId}")]
        public async Task<IActionResult> GetConcertForPerformance(int performanceId)
        {
            var concert = await _unitOfWork.Concert.GetConcertForPerformance(performanceId);
            return Ok(_mapper.Map<ConcertDTO>(concert));
        }

    }
}
