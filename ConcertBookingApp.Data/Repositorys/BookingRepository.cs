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
        public async Task<List<Booking>> GetAllBookings(int userId)
        {
            return _dbContext.Bookings.Where(x => x.UserId == userId).ToList();
        }

        public async Task<List<BookingPerformance>> GetPerformancesForBooking(int id)
        {
            return _dbContext.BookingPerformances.Where(x => x.BookingId == id).ToList();
        }
        public async Task RemoveBooking(Booking booking)
        {
            _dbContext.Bookings.Remove(booking);
            await _dbContext.SaveChangesAsync();
        }

        //TODO Remove hard coded user id
        public async Task<int> SaveBooking(Booking booking)
        {
            try
            {
                booking.UserId = 1;
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
