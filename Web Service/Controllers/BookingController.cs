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
        /// <summary>
        /// The Method that passes the booking to the repository
        /// </summary>
        /// <param name="bookingDTO">A DTO of the booking wanting to be saved</param>
        /// <returns>Status code whether it was successful or not</returns>
        [HttpPost]
        public async Task<IActionResult> SaveBooking(BookingDTO bookingDTO)
        {
            //Converts the DTO to a normal booking and sends it into the UnitOfWork
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

        /// <summary>
        /// The part of the controller that saves all tickets booked
        /// </summary>
        /// <param name="bookingPerformanceDTO">List of the tickets that should be saved</param>
        /// <returns>StatusCode whether it was successful or not</returns>
        [HttpPost("performances")]
        public async Task<IActionResult> SaveBookingPerformances([FromBody]List<BookingPerformanceDTO> bookingPerformanceDTO)
        {
            //Converts the DTO to a list of BookingPerformances further passed to the unit of work
            try
            {
                List<BookingPerformance> bookingPerformance = _mapper.Map<List<BookingPerformance>>(bookingPerformanceDTO);
                await _unitOfWork.BookingPerformance.SavePerformances(bookingPerformance);
                //After the performances are saved, the amount of seats available gets updated
                await _unitOfWork.Performance.UpdateSeats(bookingPerformance);
                return Ok("Performances Saved");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// The method that talks with the unitofwork to get all
        /// bookings associated to a user
        /// </summary>
        /// <param name="id">The user Id of current user</param>
        /// <returns>Status Code whether its successful or not</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookings(int id)
        {
            //Passes the ID on to the unit of work to get returned all bookings
            try
            {
                List<Booking> bookings = await _unitOfWork.Booking.GetAllBookings(id);
                return Ok(_mapper.Map<List<BookingDTO>>(bookings));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Method that gets all performances for a specific booking
        /// </summary>
        /// <param name="id">The booking ID</param>
        /// <returns>Status Code whether its successful or not</returns>
        [HttpGet("Performances/{id}")]
        public async Task<IActionResult> GetPerformancesForBooking(int id)
        {
            //Passes the ID on to the unit of work
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

        /// <summary>
        /// Method that Removes a performance from the database
        /// </summary>
        /// <param name="bookingPerformanceId">The ID of the performance for part of the composite key</param>
        /// <param name="bookingId">The ID of the booking for part of the composite key</param>
        /// <returns></returns>
        [HttpGet("CancelPerformance/{bookingPerformanceId}/{bookingId}")]
        public async Task<IActionResult> CancelPerformance(int bookingPerformanceId, int bookingId)
        {
            //Passes the IDs on to the unit of work
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
