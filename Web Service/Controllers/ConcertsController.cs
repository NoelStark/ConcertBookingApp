using AutoMapper;
using ConcertBookingApp.Data;
using ConcertBookingApp.DTOs;
using ConcertBookingApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Web_Service.Controllers
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
        public async Task<IActionResult> GetConcert(int id)
        {
            List<Concert> concerts = _concertRepository.GetAllConcerts();
            return Ok("Funkar");
        }

    }
}
