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
        public async Task SavePerformances(List<BookingPerformance> performances)
        {
            foreach (var performance in performances)
            {
                _dbContext.BookingPerformances.Add(performance);
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task CancelPerformance(int id, int bookingId)
        {
            var performance = _dbContext.BookingPerformances.FirstOrDefault(x => x.PerformanceId == id && x.BookingId == bookingId);
            _dbContext.BookingPerformances.Remove(performance);
        }
    }
}
