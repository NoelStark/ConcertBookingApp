using AutoMapper;
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
        private readonly ConcertRepository _concertRepository;
        private readonly IMapper _mapper;
        public ConcertsController(ConcertRepository concertRepository, IMapper mapper)
        {
            _concertRepository = concertRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetConcerts()
        {
            try
            {
                var concerts = _concertRepository.GetAllConcerts();
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
            List<Performance> performances = _concertRepository.GetPerformances(id);
            return Ok(_mapper.Map<List<PerformanceDTO>>(performances));
        }

    }
}
