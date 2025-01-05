using ConcertBookingApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBookingApp.Data.Repositorys
{
    public interface IBookingPerformanceRepository : IGenericRepository<BookingPerformance>
    {
        Task<BookingPerformance> FindBookingPerformance(BookingPerformance bookingPerfromance);
    }
}
