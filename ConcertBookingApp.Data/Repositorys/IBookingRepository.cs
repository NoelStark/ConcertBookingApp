using SharedResources.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBookingApp.Data.Repositorys
{
    public interface IBookingRepository : IGenericRepository<Booking>
    {
        Task RemoveBooking(Booking booking);
        Task<List<Booking>> GetAllBookings(int userId);
        Task<List<BookingPerformance>> GetPerformancesForBooking(int id);
        Task<int> SaveBooking(Booking booking);
    }
}
