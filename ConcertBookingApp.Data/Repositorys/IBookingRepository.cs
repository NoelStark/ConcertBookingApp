using ConcertBookingApp.Data.Models;
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
    }
}
