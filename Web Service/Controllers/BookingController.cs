using AutoMapper;
using ConcertBookingApp.Data.Database;
using Microsoft.AspNetCore.Mvc;
using Shared_Resources.DTOs;
using SharedResources.DTOs;
using SharedResources.Models;

namespace Web_Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;
        public BookingController(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> SaveBooking(BookingDTO bookingDTO)
        {
            try
            {
                Booking booking = _mapper.Map<Booking>(bookingDTO);
                booking.BookingDate = DateTime.Now;
                int bookingId = await _unitOfWork.Booking.SaveBooking(booking);
                return Ok(bookingId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost("performances")]
        public async Task<IActionResult> SaveBookingPerformances([FromBody]List<BookingPerformanceDTO> bookingPerformanceDTO)
        {
            try
            {
                List<BookingPerformance> bookingPerformance = _mapper.Map<List<BookingPerformance>>(bookingPerformanceDTO);
                await _unitOfWork.BookingPerformance.SavePerformances(bookingPerformance);
                await _unitOfWork.Performance.UpdateSeats(bookingPerformance);
                return Ok("Performances Saved");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookings(int id)
        {
            try
            {
                var bookings = await _unitOfWork.Booking.GetAllBookings(id);
                return Ok(_mapper.Map<List<BookingDTO>>(bookings));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("Performances/{id}")]
        public async Task<IActionResult> GetPerformancesForBooking(int id)
        {
            try
            {
                var bookings = await _unitOfWork.Booking.GetPerformancesForBooking(id);
                return Ok(_mapper.Map<List<BookingPerformanceDTO>>(bookings));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("CancelPerformance/{bookingPerformanceId}/{bookingId}")]
        public async Task<IActionResult> CancelPerformance(int bookingPerformanceId, int bookingId)
        {
            try
            {
                await _unitOfWork.BookingPerformance.CancelPerformance(bookingPerformanceId, bookingId);
                return Ok("Performance Cancelled");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server error");
            }
        }
    }
}
