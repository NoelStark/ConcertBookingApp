using ConcertBookingApp.Data.Database;
using ConcertBookingApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBookingApp.Data.Repositorys
{
    public interface IPerformanceRepository : IGenericRepository<Performance>
    {
        Task<Performance> FindPerformance(Performance performance);
        Task RemoveSelectedPerformance(Performance performance);
        Task<List<Performance>> GetAllPerfromances();
    }
}
