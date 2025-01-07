using ConcertBookingApp.Data.Database;
using SharedResources.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBookingApp.Data.Repositorys
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        public BookingRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Method to get all bookings for the current user
        /// </summary>
        /// <param name="userId">The unique ID of the user</param>
        /// <returns>All bookings found</returns>
        public async Task<List<Booking>> GetAllBookings(int userId)
        {
            return _dbContext.Bookings.Where(x => x.UserId == userId).ToList();
        }

        /// <summary>
        /// Method to get all performances related to a booking
        /// </summary>
        /// <param name="id">The booking id of which booking performances should be returned</param>
        /// <returns>A list of all booking performances</returns>
        public async Task<List<BookingPerformance>> GetPerformancesForBooking(int id)
        {
            return _dbContext.BookingPerformances.Where(x => x.BookingId == id).ToList();
        }

        /// <summary>
        /// Method to remove a booking from the database
        /// </summary>
        /// <param name="booking">The booking that should be remove</param>
        /// <returns></returns>
        public async Task RemoveBooking(Booking booking)
        {
            _dbContext.Bookings.Remove(booking);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Saves the booking to the database
        /// </summary>
        /// <param name="booking">The object that wishes to be saved</param>
        /// <returns>The Id that got assigned to the booking</returns>
        public async Task<int> SaveBooking(Booking booking)
        {
            try
            {
                _dbContext.Bookings.Add(booking);
                await _dbContext.SaveChangesAsync();
                return booking.BookingId;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return 0;
        }
    }
}
