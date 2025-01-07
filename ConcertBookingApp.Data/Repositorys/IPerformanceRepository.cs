using ConcertBookingApp.Data.Database;
using SharedResources.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBookingApp.Data.Repositorys
{
    public interface IPerformanceRepository : IGenericRepository<Performance>
    {
        Task<Performance> FindPerformance(int performanceId);
        Task RemoveSelectedPerformance(int performanceId);
        Task<List<Performance>> GetAllPerformances();
        Task<List<Performance>> GetPerformancesForConcert(int concertId);
        Task UpdateSeats(List<BookingPerformance> bookingPerformance);

    }
}
