using ConcertBookingApp.Data.Database;
using SharedResources.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBookingApp.Data.Repositorys
{
    public class BookingPerformanceRepository : GenericRepository<BookingPerformance>, IBookingPerformanceRepository
    {
        public BookingPerformanceRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<BookingPerformance> FindBookingPerformance(BookingPerformance bookingPerfromance)
        {
            return _dbContext.BookingPerformances.FirstOrDefault(a => a.Performance.PerformanceId == bookingPerfromance.Performance.PerformanceId);
        }
    }
}
