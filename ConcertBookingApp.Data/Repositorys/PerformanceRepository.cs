using ConcertBookingApp.Data.Database;
using ConcertBookingApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBookingApp.Data.Repositorys
{
    public class PerformanceRepository : GenericRepository<Performance>, IPerformanceRepository
    {
        public PerformanceRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<Performance> FindPerformance(Performance performance)
        {
            return await _dbContext.Performances.FirstOrDefaultAsync(a => a.PerformanceId == performance.PerformanceId);
        }

        public async Task RemoveSelectedPerformance(Performance performance)
        {
            _dbContext.Performances.Remove(performance);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Performance>> GetAllPerfromances()
        {
            return await _dbContext.Performances.ToListAsync();
        }
    }
}
