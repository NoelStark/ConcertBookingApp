using ConcertBookingApp.Data.Database;
using SharedResources.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBookingApp.Data.Repositorys
{
    public class ConcertRepository : GenericRepository<Concert>, IConcertRepository
    {
        public ConcertRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<List<Concert>> GetAllConcerts()
        {
            return _dbContext.Concerts.ToList();
        }

        public async Task<Concert> GetConcertForPerformance(int performanceId)
        {
            Concert? concert = await _dbContext.Concerts.FirstOrDefaultAsync(x => x.Performances.Any(y => y.PerformanceId == performanceId));
            if (concert == null) return new Concert();
            return concert;
        }

    }
}
