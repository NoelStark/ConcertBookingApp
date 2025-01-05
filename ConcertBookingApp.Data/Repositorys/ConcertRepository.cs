using ConcertBookingApp.Data.Database;
using ConcertBookingApp.Data.Models;
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

        public async Task<List<Concert>> GetAllPerfromances()
        {
            List<Concert> allConcerts = _dbContext.Concerts.ToList();
            return allConcerts;
        }
    }
}
